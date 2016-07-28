using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Shared.Storage
{
	public class TypeGroup
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string SourceType { get; set; }
		public Guid SourceId { get; set; }
		public List<string> Tags { get; set; }
		public List<string> SystemTags { get; set; }
	}
}
