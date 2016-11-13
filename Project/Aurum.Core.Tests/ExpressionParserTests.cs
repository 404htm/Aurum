using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aurum.Core.Parser;
using System.Text;

namespace Aurum.Core.Tests
{
	[TestClass]
	public class ExpressionParserTests
	{
		[TestMethod]
		public void SimpleStatementTests()
		{
			var runner = new ExpressionParser<Func<int, int>>();
			var result = runner.Parse("(n) => 6 + n").Result;

			Assert.IsNotNull(result);
			Assert.AreEqual(6 + 5, result(5));
		}

		[TestMethod]
		public void SimpleStatementWithImport()
		{
			var runner = new ExpressionParser<Func<Context>>();
			runner.Register<Context>();
			runner.Import("Aurum.Core");
			var result = runner.Parse("() => new Context()").Result;

			Assert.IsNotNull(result);
			Assert.IsNotNull(result());
		}

	}
}
