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
        public void TemplateRewriter_EnsurePlainTextIsntAltered()
        {
            Func<string, string> substitution = (line) => "FAIL";
            var input = new List<string> { "LINE 1", "LINE 2", "LINE 3" };

            var underTest = new TemplateRewriter(":", "`", substitution);
            var result = underTest.Rewrite(input);

            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(input.SequenceEqual(result), "Input sequence should be unaltered");
        }

        [TestMethod]
        public void TemplateRewriter_EnsureCommentTextIsntAltered()
        {
            Func<string, string> substitution = (line) => "FAIL";
            var input = new List<string> { "LINE 1", "//LINE 2", "//LINE 3" };

            var underTest = new TemplateRewriter(":", "`", substitution);
            var result = underTest.Rewrite(input);

            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(input.SequenceEqual(result), "Input sequence should be unaltered");
        }


        [TestMethod]
        public void TemplateRewriter_EnsureMetaLinesReplaced()
        {
            Func<string, string> substitution = (line) => $"-{line}-";
            var input = new List<string> { "A", ":B", "C" };

            var underTest = new TemplateRewriter(":", "`", substitution);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("-B-", result[1]);
            Assert.AreEqual("C", result[2]);
        }

        [TestMethod]
        public void TemplateRewriter_EnsureCommentMetaLinesReplaced()
        {
            Func<string, string> substitution = (line) => $"-{line}-";
            var input = new List<string> { "A", "//:B", "C" };

            var underTest = new TemplateRewriter(":", "`", substitution);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("-B-", result[1]);
            Assert.AreEqual("C", result[2]);
        }

        [TestMethod]
        public void TemplateRewriter_EnsureCommentMetaAWorksWithLeadingWhitespace()
        {
            Func<string, string> substitution = (line) => $"-{line}-";
            var input = new List<string> { "A", "    //:B", "C" };

            var underTest = new TemplateRewriter(":", "`", substitution);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("-B-", result[1]);
            Assert.AreEqual("C", result[2]);
        }

        [TestMethod]
        public void TemplateRewriter_EnsurePreservesLeadingWhitespace()
        {
            Func<string, string> substitution = (line) => $"-{line}-";
            var input = new List<string> { "A", ":    B", "C" };

            var underTest = new TemplateRewriter(":", "`", substitution);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("-    B-", result[1]);
            Assert.AreEqual("C", result[2]);
        }

        [TestMethod]
        public void TemplateRewriter_EnsureOnlyWhitespaceAllowedBeforeMeta()
        {
            Func<string, string> substitution = (line) => $"-{line}-";
            var input = new List<string> { "A", ":    B", ".    :C" };

            var underTest = new TemplateRewriter(":", "`", substitution);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("-    B-", result[1]);
            Assert.AreEqual(".    :C", result[2]);
        }

        [TestMethod]
        public void TemplateRewriter_EnsureMetaAWorksWithLeadingTabs()
        {
            Func<string, string> substitution = (line) => $"-{line}-";
            var input = new List<string> { "A", ":B", "\t:C" };

            var underTest = new TemplateRewriter(":", "`", substitution);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("-B-", result[1]);
            Assert.AreEqual("-C-", result[2]);
        }

        [TestMethod]
        public void TemplateRewriter_EnsureMetaLinesReplacedCustomValueRegexUnsafe()
        {
            Func<string, string> substitution = (line) => $"-{line}-";
            var input = new List<string> { "A", "^B", "C" };

            var underTest = new TemplateRewriter("^", "`", substitution);
            var result = underTest.Rewrite(input).ToList();

            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("A", result[0]);
            Assert.AreEqual("-B-", result[1]);
            Assert.AreEqual("C", result[2]);
        }
    }
}
