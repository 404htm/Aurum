using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;
using System.IO;

namespace Aurum.Core.Tests
{
	[TestClass]
	public class StoreableBaseTests
	{
		[DataContract(IsReference = false)]
		private class TestObjA : StoreableBase<TestObjA>
		{
			[DataMember] public string Name { get; set; }
			[DataMember] public int Test { get; set; }
			[DataMember] public DateTime date { get; set; }
		}

		[TestMethod]
		public void SaveAndReload()
		{
			var obj = new TestObjA { Name = "TEST", Test = 5, date = DateTime.Now };
			var tmp = Path.GetTempFileName();

			obj.Save(tmp);
			var copy = TestObjA.Load(tmp);

			Assert.IsNotNull(copy);
			Assert.AreEqual(obj.Name, copy.Name);
			Assert.AreEqual(obj.Test, copy.Test);
			Assert.AreEqual(0, obj.date.Subtract(copy.date).Milliseconds,  "Serialized and deserialized dates do not match");

			File.Delete(tmp);
		}

		
	}
}
