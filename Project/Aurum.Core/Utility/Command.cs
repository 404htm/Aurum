using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aurum.Core.Utility
{
	public class Command
	{
		public Command(string command, Func<Expression, Expression, Expression> exp)
		{
			Pattern = new Regex(command);
			Expression = exp;
		}
		public Regex Pattern { get; private set; }
		public Func<Expression, Expression, Expression> Expression { get; private set; }

		public Expression Parse(string expression, Func<string, Func<Expression>> subparser)
		{
			return null;
		}
	}

}
