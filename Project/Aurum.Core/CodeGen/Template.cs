using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core.CodeGen
{
	[DataContract]
	public class Template : TemplateComponent
	{
		public Template(List<TemplateComponent> components) : base(components)
		{
		}

		public string FileName { get; set; }
		public List<String> References { get; set; }
		
	}
}
