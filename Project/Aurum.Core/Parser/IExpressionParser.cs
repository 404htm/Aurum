using System;
using System.Threading.Tasks;

namespace Aurum.Core.Parser
{
	public interface IExpressionParser<T>
	{
		Task<T> Parse(string expression);
		void Import(string v);
		void Register<T2>();
	}
}