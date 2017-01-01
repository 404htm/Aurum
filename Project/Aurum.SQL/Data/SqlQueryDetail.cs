using Aurum.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL.Data
{
    public class SqlQueryDetail : SqlQueryDefinition
    {
        [DataMember] public IList<string> ExtendedInfo { get; set; }
        [DataMember] public IList<SqlParameter> Inputs { get; internal set; }
        [DataMember] public IList<SqlColumn> Outputs { get; internal set; }

        internal static SqlQueryDetail MapFrom(SqlQueryDefinition original)
        {
            var result = new SqlQueryDetail();
            result.Description = original.Description;
            result.GroupName = original.GroupName;
            result.Id = original.Id;
            result.IsModified = original.IsModified;
            result.Name = original.Name;
            result.Query = original.Query;
            result.SourceName = original.SourceName;
            result.SourceType = original.SourceType;
            return result;
        }
    }
}
