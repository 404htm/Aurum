using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aurum.Core.Utility;

namespace Aurum.Core.Tests
{
	[TestClass]
	public class ExpressionParserTests
	{
		[TestMethod]
		public void SimpleStatementTests()
		{
			var runner = new ExpressionParser<Func<int, int>>();
			var result = runner.Parse("(n) => 6 + n");

			Assert.IsNotNull(result);
			Assert.AreEqual(6 + 5, result(5));
		}
	}
}
