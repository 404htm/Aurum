using System;

namespace Aurum.Core.Parser
{
	public interface IExpressionParser<T>
	{
		T Parse(string expression);
		void Import(string v);
		void Register<T2>();
	}
}