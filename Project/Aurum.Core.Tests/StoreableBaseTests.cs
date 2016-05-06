using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Generic;
using Aurum.Core;

namespace Aurum.Core.Tests
{
	[TestClass]
	public class StoreableBaseTests
	{
		[DataContract(IsReference = false)]
		private class TestObjA : Storeable<TestObjA>
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

		[TestMethod]
		public void SaveAndReloadSet()
		{
			var count = 5;
			var list = new List<TestObjA>();
			var tmp = Path.GetTempFileName();

			for (int i = 0; i < count; i++) list.Add(new TestObjA { Name = $"TEST{i}", Test = i, date = DateTime.Now });

			new StoreableSet<TestObjA>(list).Save(tmp);
			var copy = StoreableSet<TestObjA>.Load(tmp);

			Assert.IsNotNull(copy);
			Assert.AreEqual(copy.Count, list.Count);

			for (int i = 0; i < count; i++)
			{
				Assert.AreEqual(list[i].Name, copy[i].Name);
				Assert.AreEqual(list[i].Test, copy[i].Test);
				Assert.AreEqual(0, list[i].date.Subtract(copy[i].date).Milliseconds, "Serialized and deserialized dates do not match");
			}

			File.Delete(tmp);
		}
	}
}
