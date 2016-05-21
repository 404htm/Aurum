using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core.Utility
{
	public class ExpressionCompiler<T>
	{
		List<ParameterExpression> _parameters = new List<ParameterExpression>();
		List<Expression> _variables = new List<Expression>();

		public void SetParameter<P>(string name)
		{
			_parameters.Add(Expression.Parameter(typeof(P), name));
		}



		//	public void AddCapture<T>(string name, T obj)
		//	{
		//		var @const = Expression.Constant(obj, typeof(T));
		//		var @var = Expression.Variable(typeof(T), name);
		//		var assignment = Expression.Assign(@var, @const);
		//		_variables.Add(@var);
		//		_boilerplate.Add(assignment);
		//	}
		//	

		public T CompileStatement(string statement)
		{
			var block = parse(statement);
			var result = Expression.Lambda<T>(block, _parameters.ToArray()).Compile();
			return result;
		}

		private BlockExpression parse(string statement)
		{
			var block = Expression.Block(Expression.Equal(Expression.Constant(5), Expression.Constant(5)));
			return block;
		}
	}
}
