using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Generic;

namespace Aurum.Core.Tests
{
    [TestClass]
	public class StoreableBaseTests
	{
		[DataContract(IsReference = false)]
		private class TestObj : Storeable<TestObj>
		{
			[DataMember] public string Name { get; set; }
			[DataMember] public int Test { get; set; }
			[DataMember] public DateTime date { get; set; }
		}

		private void AssertCompare(TestObj original, TestObj copy)
		{
			Assert.IsNotNull(copy);
			Assert.AreEqual(original.Name, copy.Name);
			Assert.AreEqual(original.Test, copy.Test);
			Assert.AreEqual(0, original.date.Subtract(copy.date).Milliseconds, "Serialized and deserialized dates do not match");
		}

		[TestMethod]
		public void SaveAndReload()
		{
			var tmp = Path.GetTempFileName();
			var obj = new TestObj { Name = "TEST", Test = 5, date = DateTime.Now };

			obj.Save(tmp);
			var copy = TestObj.Load(tmp);
			AssertCompare(obj, copy);

			File.Delete(tmp);
		}

		[TestMethod]
		public void SaveAndReloadSet()
		{
			var count = 5;
			var list = new List<TestObj>();
			var tmp = Path.GetTempFileName();
			for (int i = 0; i < count; i++) list.Add(new TestObj { Name = $"TEST{i}", Test = i, date = DateTime.Now });

			new StoreableSet<TestObj>(list).Save(tmp);
			var copy = StoreableSet<TestObj>.Load(tmp);

			Assert.IsNotNull(copy);
			Assert.AreEqual(copy.Count, list.Count);
			for (int i = 0; i < count; i++) AssertCompare(list[i], copy[i]);

			File.Delete(tmp);
		}

		[TestMethod]
		public void ResaveSet()
		{
			var count = 5;
			var list = new List<TestObj>();
			var tmp = Path.GetTempFileName();
			for (int i = 0; i < count; i++) list.Add(new TestObj { Name = $"TEST{i}", Test = i, date = DateTime.Now });

			//Ensure that the file isn't appending
			new StoreableSet<TestObj>(list).Save(tmp);
			var copy = StoreableSet<TestObj>.Load(tmp);
			new StoreableSet<TestObj>(list).Save(tmp);
			copy = StoreableSet<TestObj>.Load(tmp);

			Assert.AreEqual(list.Count, copy.Count);
			File.Delete(tmp);
		}
	}
}
