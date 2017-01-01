using Aurum.Integration.Tests.Temp;
using Aurum.TemplateUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Integration.Tests.Templates.datalayer_basic
{
    public class Repository : ITemplate<GroupMetadata>
    {
        public string GetFileName(GroupMetadata group)
        {
            return $"{group.Name}Repository.cs";
        }

        public void GenerateCode(GroupMetadata group, ICodeEmitter emitter)
        {
            //:using System;
            //:namespace Aurum.Generated
            //:{
            //:    public class `group.Name`Repository
            //:    {
            //:
            foreach (var op in group.Operations)
            {
                //:         public 'op.TypeName.ToSafeNameCS()' 'column.DisplayName' {get; set;}
                //:
            }
            //:    }
            //:
            //:}
        }
    }
}
