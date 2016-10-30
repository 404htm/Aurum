using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core.CodeGen
{
	public class TemplateComponent
	{
		private List<TemplateComponent> components;

		public TemplateComponent(List<TemplateComponent> components)
		{
			this.components = components;
		}

		public List<TemplateComponent> Content { get; set; }
	}
}
