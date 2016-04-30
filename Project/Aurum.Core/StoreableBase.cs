using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Aurum.Core
{
	[DataContract(Namespace = "", IsReference = true)]
	public abstract class StoreableBase<T> where T : StoreableBase<T>
	{
		public void Save(string filename)
		{
			var ser = new DataContractSerializer(typeof(T));
			var xml = new XmlWriterSettings { Indent = true, NewLineHandling = NewLineHandling.Replace, NewLineChars = Environment.NewLine };
			using (var writer = XmlWriter.Create(filename)) ser.WriteObject(writer, (T)this);
		}
		public static T Load(string filename)
		{
			var ser = new DataContractSerializer(typeof(T));
			T result;
			using (var reader = XmlReader.Create(filename)) result = (T)ser.ReadObject(reader);
			result.OnDeserialization();
			return result;
		}
		public static T LoadFromXML(string xml)
		{
			var ser = new DataContractSerializer(typeof(T));
			T result;
			using (var reader = XmlReader.Create(new StringReader(xml))) result = (T)ser.ReadObject(reader);
			result.OnDeserialization();
			return result;
		}
		public XElement ToXElement()
		{
			var ser = new DataContractSerializer(typeof(T));
			var xml = new XmlWriterSettings { Indent = true, NewLineHandling = NewLineHandling.Replace, NewLineChars = Environment.NewLine };
			var ms = new MemoryStream();
			using (var writer = XmlWriter.Create(ms)) ser.WriteObject(writer, (T)this);
			ms.Position = 0;
			return XElement.Load(ms);
		}
		protected virtual void OnDeserialization()
		{
		}
	}
}
