using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core.Parser
{
	public class ParserFactory : IParserFactory
	{
		IExpressionParser<T> IParserFactory.Create<T>()
		{
			var result = new ExpressionParser<T>();
			result.Register(typeof(Queryable));
			return result;
		}
	}
}
