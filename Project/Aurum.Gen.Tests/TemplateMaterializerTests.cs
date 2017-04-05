using Aurum.Core.Parser;
using Aurum.Gen.Tests.Metadata;
using Aurum.TemplateUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using StrFunc = System.Func<string, string>;

namespace Aurum.Gen.Tests
{
    [TestClass]
    public class TemplateMaterializerTests
    {
        [TestMethod]
        public void VerifyMaterializer()
        {
            var nl = Environment.NewLine;
            var source = new string[] { "Line1", "Line2", "Line3", "Line4" };
            var expectedTemplateTxt = $"Line1;{nl}Line2;{nl}Line3;{nl}Line4;{nl}";

            var templateMock = new Mock<ITemplate<BasicTable>>();

            var rewriterMock = new Mock<ITemplateRewriter>();
            rewriterMock
                .Setup(λ => λ.Rewrite(It.IsAny<IEnumerable<string>>()))
                .Returns<IEnumerable<string>>(set => set.Select(λ => λ + ";"))
                .Verifiable();

            //We need to verify that parser is called with the rewritten template text
            var parserMock = new Mock<IParser<ITemplate<BasicTable>>>();
            parserMock
                .Setup(λ => λ.Parse(It.IsAny<string>()))
                .Callback<string>(λ => Assert.AreEqual(expectedTemplateTxt, λ))
                .Returns<string>(λ => Task.FromResult(templateMock.Object))
                .Verifiable();

            //Ugh... Factories.
            var rewriterFactoryMock = new Mock<ITemplateRewriterFactory>();
            rewriterFactoryMock
                .Setup(λ => λ.Create(It.IsAny<StrFunc>(), It.IsAny<StrFunc>()))
                .Returns<StrFunc, StrFunc>((m, i) => rewriterMock.Object);

            var parserFactoryMock = new Mock<IParserFactory>();
            parserFactoryMock
                .Setup(λ => λ.Create<ITemplate<BasicTable>>())
                .Returns(() => parserMock.Object);

            //Run the materializer - Checking that the intermidate steps are called and the right type is returned
            var underTest = new TemplateMaterializer<BasicTable>(source, rewriterFactoryMock.Object, parserFactoryMock.Object);
            var result = underTest.Build().Result;
            Assert.IsInstanceOfType(result, typeof(ITemplate<BasicTable>));
            Mock.VerifyAll(new Mock[] { rewriterMock, parserMock});

            //TODO: Ensure that proper delegates are sent to rewriter - verify emitter name

        }
    }
}
