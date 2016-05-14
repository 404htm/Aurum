using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Aurum.Core
{
	[DataContract(Namespace = "", IsReference = false)]
	public abstract class Storeable<T> where T : Storeable<T>
	{
		public void Save(string filename)
		{
			var ser = new DataContractJsonSerializer(typeof(T));
			using (FileStream stream = File.Create(filename))
			{
				ser.WriteObject(stream, this);
			}
		}

		public static T Load(string filename)
		{
			using (FileStream stream = File.OpenRead(filename))
			{
				return Load(stream);
			}
		}

		public static T Load(Stream stream)
		{
			var ser = new DataContractJsonSerializer(typeof(T));
			T result = (T)ser.ReadObject(stream);
			result.OnDeserialization();
			return result;
		}
		
		protected virtual void OnDeserialization()
		{
		}
	}
}
