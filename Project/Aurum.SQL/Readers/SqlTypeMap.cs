using Aurum.SQL.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using DbTypeLookup = System.Collections.Generic.Dictionary<Aurum.SQL.Data.SqlType, System.Type>;

namespace Aurum.SQL
{
    public class SqlTypeMap
    {
        //Reference: https://msdn.microsoft.com/en-us/library/system.data.SqlType(v=vs.110).aspx

        static readonly DbTypeLookup _SqlTypeLookup;

        static SqlTypeMap()
        {
            _SqlTypeLookup = BuildDbTypeLookup(); 
        }

        static DbTypeLookup BuildDbTypeLookup()
        {
            return new Dictionary<SqlType, Type>
            {
                {SqlType.BigInt,          typeof(long) },
                {SqlType.Binary,          typeof(byte[]) },
                {SqlType.Bit,             typeof(bool) },
                {SqlType.NVarChar,        typeof(string) },
                {SqlType.VarChar,         typeof(string) },
                {SqlType.Char,            typeof(string) },
                {SqlType.Date,            typeof(DateTime) },
                {SqlType.DateTime,        typeof(DateTime) },
                {SqlType.DateTime2,       typeof(DateTime) },
                {SqlType.DateTimeOffset,  typeof(TimeSpan) }, //TODO: Validate this decision
                {SqlType.Decimal,         typeof(decimal) },
                {SqlType.Float,           typeof(float) },
                {SqlType.Image,           typeof(byte[]) },
                {SqlType.Int,             typeof(int) },
                {SqlType.Money,           typeof(decimal) },
                {SqlType.NChar,           typeof(string) },
                {SqlType.NText,           typeof(string) },
                {SqlType.Real,            typeof(Single) },
                {SqlType.SmallDateTime,   typeof(DateTime) },
                {SqlType.SmallInt,        typeof(Int16) },
                {SqlType.SmallMoney,      typeof(Decimal) },
                //{SqlType.Structured,      null },
                {SqlType.Text,            typeof(string) },
                {SqlType.Time,            typeof(DateTime) },
                {SqlType.Timestamp,       typeof(byte[]) },
                {SqlType.TinyInt,         typeof(byte) },
                {SqlType.Udt,             null },
                {SqlType.UniqueIdentifier,typeof(Guid) },
                {SqlType.VarBinary,       typeof(byte[]) },
                {SqlType.Sql_Variant,         typeof(object) },
                {SqlType.Xml,             typeof(XElement) }
            };
        }

        public static SqlType Get(Type type) => _SqlTypeLookup.Where(l => l.Value == type).Select(l => l.Key).FirstOrDefault();
    }
}
