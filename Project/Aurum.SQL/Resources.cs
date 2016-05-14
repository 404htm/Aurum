using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL
{
	public class Resources
	{
		public static Stream GetDefaultTemplates()
		{
			var asm = Assembly.GetExecutingAssembly();
			var stream = asm.GetManifestResourceStream("Aurum.SQL.DefaultSqlTemplates.json");
			return stream;
		}
	}
}
