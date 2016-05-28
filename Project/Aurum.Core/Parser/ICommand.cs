using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core.Parser
{
	public interface ICommand
	{
		Expression Parse(string statement);

	}
}
