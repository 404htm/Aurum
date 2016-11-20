using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;
using Aurum.Gen.Nodes;
using Aurum.Core.Parser;
using System.Collections.Generic;
using Aurum.Core;


namespace Aurum.Gen.Tests
{
    [TestClass]
    public class TemplateRewriterTests
    {
        [TestMethod]
        public void EnsurePlainTextIsntAltered()
        {
            Func<string, string> metaSub = (line) => "FAIL";
            Func<string, string> inlineSub = (statement) => "X";

            var input = new List<string> { "LINE 1", "LINE 2", "LINE 3" };

            var underTest = new TemplateRewriter(":", "`", "`", metaSub, inlineSub);
            var result = underTest.Rewrite(input);

            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(input.SequenceEqual(result), "Input sequence should be unaltered");
        }

        [TestMethod]
        public void EnsureCommentTextIsntAltered()
        {
            Func<string, string> metaSub = (line) => "FAIL";
            Func<string, string> inlineSub = (statement) => "X";

            var input = new List<string> { "LINE 1", "//LINE 2", "//LINE 3" };

            var underTest = new TemplateRewriter(":", "`", "`", metaSub, inlineSub);
            var result = underTest.Rewrite(input);

            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(input.SequenceEqual(result), "Input sequence should be unaltered");
        }


        [TestMethod]
        public void EnsureMetaLinesReplaced()
        {
            Func<string, string> metaSub = (line) => $"-{line}-";
            Func<string, string> inlineSub = (statement) => "X";
            var input = new List<string> { "A", ":B", "C" };

            var underTest = new TemplateRewriter(":", "`", "`", metaSub, inlineSub);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("-B-", result[1]);
            Assert.AreEqual("C", result[2]);
        }

        [TestMethod]
        public void EnsureCommentMetaLinesReplaced()
        {
            Func<string, string> metaSub = (line) => $"-{line}-";
            Func<string, string> inlineSub = (statement) => "X";
            var input = new List<string> { "A", "//:B", "C" };

            var underTest = new TemplateRewriter(":", "`", "`", metaSub, inlineSub);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("-B-", result[1]);
            Assert.AreEqual("C", result[2]);
        }

        [TestMethod]
        public void EnsureCommentMetaAWorksWithLeadingWhitespace()
        {
            Func<string, string> metaSub = (line) => $"-{line}-";
            Func<string, string> inlineSub = (statement) => "X";
            var input = new List<string> { "A", "    //:B", "C" };

            var underTest = new TemplateRewriter(":", "`", "`", metaSub, inlineSub);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("-B-", result[1]);
            Assert.AreEqual("C", result[2]);
        }

        [TestMethod]
        public void EnsurePreservesLeadingWhitespace()
        {
            Func<string, string> metaSub = (line) => $"-{line}-";
            Func<string, string> inlineSub = (statement) => "X";
            var input = new List<string> { "A", ":    B", "C" };

            var underTest = new TemplateRewriter(":", "`", "`", metaSub, inlineSub);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("-    B-", result[1]);
            Assert.AreEqual("C", result[2]);
        }

        [TestMethod]
        public void EnsureOnlyWhitespaceAllowedBeforeMeta()
        {
            Func<string, string> metaSub = (line) => $"-{line}-";
            Func<string, string> inlineSub = (statement) => "X";
            var input = new List<string> { "A", ":    B", ".    :C" };

            var underTest = new TemplateRewriter(":", "`", "`", metaSub, inlineSub);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("-    B-", result[1]);
            Assert.AreEqual(".    :C", result[2]);
        }

        [TestMethod]
        public void EnsureMetaWorksWithLeadingTabs()
        {
            Func<string, string> metaSub = (line) => $"-{line}-";
            Func<string, string> inlineSub = (statement) => "X";
            var input = new List<string> { "A", ":B", "\t:C" };

            var underTest = new TemplateRewriter(":", "`", "`", metaSub, inlineSub);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("-B-", result[1]);
            Assert.AreEqual("-C-", result[2]);
        }

        [TestMethod]
        public void EnsureMetaLinesReplacedCustomValueRegexUnsafe()
        {
            Func<string, string> metaSub = (line) => $"-{line}-";
            Func<string, string> inlineSub = (statement) => "X";
            var input = new List<string> { "A", "^B", "C" };

            var underTest = new TemplateRewriter("^", "`", "`", metaSub, inlineSub);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("-B-", result[1]);
            Assert.AreEqual("C", result[2]);
        }

        [TestMethod]
        public void EnsureInliningAppliedToMetaCode()
        {
            Func<string, string> metaSub = (line) => $"{line}";
            Func<string, string> inlineSub = (statement) =>  $"{{{statement}}}";
            var input = new List<string> { ":`A` and `B`" };

            var underTest = new TemplateRewriter(":", "`", "`", metaSub, inlineSub);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("{A} and {B}", result[0]);
        }

        [TestMethod]
        public void EnsureInliningNotAppliedToOuterCode()
        {
            Func<string, string> metaSub = (line) => $"{line}";
            Func<string, string> inlineSub = (statement) => $"{{{statement}}}";
            var input = new List<string> { "`A` and `B`" };

            var underTest = new TemplateRewriter(":", "`", "`", metaSub, inlineSub);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("`A` and `B`", result[0]);
        }

        [TestMethod]
        public void EnsureOuterInliningEscapesRespected()
        {
            Func<string, string> metaSub = (line) => $"{line}";
            Func<string, string> inlineSub = (statement) => $"{{{statement}}}";
            var input = new List<string> { @":`A` and `B` but not \`D\`" };

            var underTest = new TemplateRewriter(":", "`", "`", metaSub, inlineSub);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("{A} and {B} but not `D`", result[0]);
        }

        [TestMethod]
        public void EnsureCustomInliningEscapesApplied()
        {
            Func<string, string> metaSub = (line) => $"{line}";
            Func<string, string> inlineSub = (statement) => $"{{{statement}}}";
            var input = new List<string> { @":#A# and #B#" };

            var underTest = new TemplateRewriter(":", "#", "#", metaSub, inlineSub);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("{A} and {B}", result[0]);
        }

        [TestMethod]
        public void EnsureAsymetricCustomInliningEscapesApplied()
        {
            Func<string, string> metaSub = (line) => $"{line}";
            Func<string, string> inlineSub = (statement) => $"{{{statement}}}";
            var input = new List<string> { @":<=A=> and <=B=>" };

            var underTest = new TemplateRewriter(":", "<=", "=>", metaSub, inlineSub);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("{A} and {B}", result[0]);
        }

        [TestMethod]
        public void EnsureCustomeInliningEscapesAppliedRegexUnsafe()
        {
            Func<string, string> metaSub = (line) => $"{line}";
            Func<string, string> inlineSub = (statement) => $"{{{statement}}}";
            var input = new List<string> { @":(A) and (B)" };

            var underTest = new TemplateRewriter(":", "(", ")", metaSub, inlineSub);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("{A} and {B}", result[0]);
        }

        [TestMethod]
        public void ApplyRealisticTransform()
        {
            Func<string, string> metaSub = (line) => $@"Console.WriteLine($""{line}"")";
            Func<string, string> inlineSub = (statement) => $"{{{statement}}}";
            var input = new List<string> { @"   :   `A` + `B` + `C`" };

            var underTest = new TemplateRewriter(":", "`", "`", metaSub, inlineSub);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(@"Console.WriteLine($""   {A} + {B} + {C}"")", result[0]);
        }
    }
}
