using Aurum.Gen.Tests.Metadata;
using Aurum.TemplateUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen.Tests
{
    [TestClass]
    public class TemplateBaseTests
    {
        //Nested class to serve as a simple template - This replicates template post-rewrite
        private class DerivedTemplate : TemplateBase<BasicTable>
        {
            public override void GenerateCode(BasicTable model, ICodeEmitter emitter)
            {
                foreach(var column in model.Columns)
                {
                    emitter.WriteLine($"TEXT {column.DisplayName};", 10);
                }
            }

            public override string GetFileName(BasicTable model)
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void VerifyDerivedTemplateWorkflow()
        {
            //There isn't really much to test in template base
            //This is as much a workflow sanity check as anything
            //One template - multiple models with a registered emitter

            var model1 = new BasicTable();
            model1.Columns.Add(new BasicColumn { DisplayName = "A" });
            model1.Columns.Add(new BasicColumn { DisplayName = "B" });
            model1.Columns.Add(new BasicColumn { DisplayName = "C" });

            var model2 = new BasicTable();
            model2.Columns.Add(new BasicColumn { DisplayName = "a" });
            model2.Columns.Add(new BasicColumn { DisplayName = "b" });
            model2.Columns.Add(new BasicColumn { DisplayName = "c" });
            model2.Columns.Add(new BasicColumn { DisplayName = "d" });

            var output = new StringBuilder();
            var emitter = new Mock<ICodeEmitter>();
            emitter.Setup(λ => λ.WriteLine(It.IsAny<string>(), It.IsAny<int>())).Callback<string, int>((text, line) => output.AppendLine($"{line} : {text}"));
            var template = new DerivedTemplate();

            var _ = Environment.NewLine;
            var expected1 = $"10 : TEXT A;{_}10 : TEXT B;{_}10 : TEXT C;{_}";
            var expected2 = $"10 : TEXT a;{_}10 : TEXT b;{_}10 : TEXT c;{_}10 : TEXT d;{_}";

            template.GenerateCode(model1, emitter.Object);
            var result1 = output.ToString();
            Assert.AreEqual(expected1, result1);

            output.Clear();

            template.GenerateCode(model2, emitter.Object);
            var result2 = output.ToString();
            Assert.AreEqual(expected2, result2);
        }
    }
}
