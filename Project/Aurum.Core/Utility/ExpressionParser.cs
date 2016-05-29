using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core.Utility
{
	public class ExpressionParser<T> : IExpressionParser<T>
	{
		public T Parse(string expression)
		{
			var script = CSharpScript.Create<T>($"return {expression};");
			var task = script.RunAsync().Result;
			return task.ReturnValue;
		}
	}
}
