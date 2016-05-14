using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL.Tests
{
	public static class TestHelpers
	{
		public static string GetTestConnection()
		{
			var env_cnn = Environment.GetEnvironmentVariable("TestConnection");
			var app_cnn = Properties.Settings.Default.cnn_db;

			return env_cnn ?? app_cnn;
		}

		public static string Escape(this string str)
		{
			return str.Replace("{", "{{").Replace("}", "}}");
		}
	}
}
