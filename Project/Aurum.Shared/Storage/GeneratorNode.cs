using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Shared.Storage
{
	public class GeneratorNode
	{
		public Guid Id { get; set; }
		public Guid ContextId { get; set; }
		public List<Partition> Partitions { get; set; }


	}
}
