using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL.Data
{
    /// <summary> Possible SQL Type values - Maps to sys.types (System.Data.SqlDbType IDs do not).</summary>
    [Serializable]
    public enum SqlType : byte
    {
        [EnumMember] Udt = 0,
        [EnumMember] Image = 34,
        [EnumMember] Text = 35,
        [EnumMember] UniqueIdentifier = 36,
        [EnumMember] Date = 40,
        [EnumMember] Time = 41,
        [EnumMember] DateTime2 = 42,
        [EnumMember] DateTimeOffset = 43,
        [EnumMember] TinyInt = 48,
        [EnumMember] SmallInt = 52,
        [EnumMember] Int = 56,
        [EnumMember] SmallDateTime = 58,
        [EnumMember] Real = 59,
        [EnumMember] Money = 60,
        [EnumMember] DateTime = 61,
        [EnumMember] Float = 62,
        [EnumMember] Sql_Variant = 98,
        [EnumMember] NText = 99,
        [EnumMember] Bit = 104,
        [EnumMember] Decimal = 106,
        [EnumMember] Numeric = 108,
        [EnumMember] SmallMoney = 122,
        [EnumMember] BigInt = 127,
        [EnumMember] HierarchyId = 240,
        [EnumMember] Geometry = 240,
        [EnumMember] Geography = 240,
        [EnumMember] VarBinary = 165,
        [EnumMember] VarChar = 167,
        [EnumMember] Binary = 173,
        [EnumMember] Char = 175,
        [EnumMember] Timestamp = 189,
        [EnumMember] NVarChar = 231,
        [EnumMember] NChar = 239,
        [EnumMember] Xml = 241,
        [EnumMember] SysName = 231
    }
}
