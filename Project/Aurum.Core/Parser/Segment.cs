using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core.Parser
{
	public class Segment : ICommand
	{
		public Segment(string start, string end)
		{

		}

		public Expression Parse(string statement)
		{
			throw new NotImplementedException();
		}
	}
}
