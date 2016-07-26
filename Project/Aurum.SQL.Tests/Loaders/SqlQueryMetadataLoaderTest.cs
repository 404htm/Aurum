﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Aurum.SQL.Loaders;
using Aurum.SQL.Readers;
using Aurum.SQL.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Aurum.SQL.Tests.Loaders
{
	[TestClass]
	public class SqlQueryMetadataLoaderTest
	{
		[TestMethod]
		public void LoadQueryMetadata()
		{
			IList<SqlError> errors = new List<SqlError>();

			var query = new SqlQueryDefinition
			{
				Name = "Test",
				Description = "Description",
				GroupName = "Group",
				Id = Guid.NewGuid(),
				IsModified = false,
				Query = "QUERY",
				SourceName = "Source",
				SourceType = Core.SourceType.Autogenerated
			};

			var reader = new Mock<ISqlQueryReader>();
			reader.Setup(λ => λ.GetParameters(query.Query, out errors)).Returns(new List<Data.SqlParameter> { new Data.SqlParameter{ Name = "TestParameter" } });
			reader.Setup(λ => λ.GetResultSet(query.Query, out errors)).Returns(new List<Data.SqlColumn> { new Data.SqlColumn { Name = "TestColumn" } });

			var metadata = new SqlQueryMetadataLoader(reader.Object);
			var result = metadata.LoadQueryDetails(query);

			Assert.IsNotNull(result);
			Assert.AreEqual(query.Query, result.Query);
			Assert.AreEqual(query.Name, result.Name);
			Assert.AreEqual(query.SourceName, result.SourceName);
			Assert.AreEqual(query.SourceType, result.SourceType);

			Assert.AreEqual(1, result.Inputs.Count);
			Assert.AreEqual(1, result.Outputs.Count);

			Assert.AreEqual("TestParameter", result.Inputs.Single().Name);
			Assert.AreEqual("TestColumn", result.Outputs.Single().Name);
		}


	}
}
