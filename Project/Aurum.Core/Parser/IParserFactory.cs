using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core.Parser
{
	public interface IParserFactory
	{
		IExpressionParser<T> Create<T>();
	}
}
