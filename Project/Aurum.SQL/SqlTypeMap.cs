using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DbTypeLookup = System.Collections.Generic.Dictionary<System.Data.SqlDbType, System.Type>;

namespace Aurum.SQL
{
	public class SqlTypeMap
	{
		//Reference: https://msdn.microsoft.com/en-us/library/system.data.sqldbtype(v=vs.110).aspx

		static readonly DbTypeLookup _sqlDbTypeLookup;

		static SqlTypeMap()
		{
			_sqlDbTypeLookup = BuildDbTypeLookup();
		}

		static DbTypeLookup BuildDbTypeLookup()
		{
			return new Dictionary<SqlDbType, Type>
			{
				{SqlDbType.BigInt,          typeof(long) },
				{SqlDbType.Binary,          typeof(byte[]) },
				{SqlDbType.Bit,             typeof(bool) },
				{SqlDbType.Char,            typeof(string) },
				{SqlDbType.Date,            typeof(DateTime) },
				{SqlDbType.DateTime,        typeof(DateTime) },
				{SqlDbType.DateTime2,       typeof(DateTime) },
				{SqlDbType.DateTimeOffset,  typeof(TimeSpan) }, //TODO: Validate this decision
				{SqlDbType.Decimal,         typeof(decimal) },
				{SqlDbType.Float,           typeof(float) },
				{SqlDbType.Image,           typeof(byte[]) },
				{SqlDbType.Int,             typeof(int) },
				{SqlDbType.Money,           typeof(decimal) },
				{SqlDbType.NChar,           typeof(string) },
				{SqlDbType.NText,           typeof(string) },
				{SqlDbType.NVarChar,        typeof(string) },
				{SqlDbType.Real,            typeof(Single) },
				{SqlDbType.SmallDateTime,   typeof(DateTime) },
				{SqlDbType.SmallInt,        typeof(Int16) },
				{SqlDbType.SmallMoney,      typeof(Decimal) },
				{SqlDbType.Structured,      null },
				{SqlDbType.Text,            typeof(string) },
				{SqlDbType.Time,            typeof(DateTime) },
				{SqlDbType.Timestamp,       typeof(byte[]) },
				{SqlDbType.TinyInt,         typeof(byte) },
				{SqlDbType.Udt,             null },
				{SqlDbType.UniqueIdentifier,typeof(Guid) },
				{SqlDbType.VarBinary,       typeof(byte[]) },
				{SqlDbType.VarChar,         typeof(string) },
				{SqlDbType.Variant,         typeof(object) },
				{SqlDbType.Xml,             typeof(XElement) }
			};
		}

	}
}
