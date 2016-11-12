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
		public async void SimpleStatementTests()
		{
			var runner = new ExpressionParser<Func<int, int>>();
			var result = await runner.Parse("(n) => 6 + n");

			Assert.IsNotNull(result);
			Assert.AreEqual(6 + 5, result(5));
		}

		[TestMethod]
		public async void SimpleStatementWithImport()
		{
			var runner = new ExpressionParser<Func<Context>>();
			runner.Register<Context>();
			runner.Import("Aurum.Core");
			var result = await runner.Parse("() => new Context()");

			Assert.IsNotNull(result);
			Assert.IsNotNull(result());
		}

	}
}
