using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Aurum.Core
{
	[DataContract]
	public class TypeDefinition : Storeable<TypeDefinition>
	{
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public Type SimpleType { get; set; }
		[DataMember]
		public Dictionary<string, TypeDefinition> Properties { get; set; }
	}
}
