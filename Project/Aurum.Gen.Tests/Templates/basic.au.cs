using Aurum.TemplateUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurum.Gen.Tests.Metadata;

namespace Aurum.Gen.Tests.Templates
{

    public class basic : ITemplate<Metadata.BasicTable>
    {
        public string GetFileName(BasicTable data)
        {
            return $"{data.Name}.cs";
        }

        public void Body(BasicTable data)
        {
            //:using System;
            //:namespace Aurum.Generated
            //:{
            //:    public class generated_`data.Name`
            //:    {
            //:
            foreach (var column in data.Columns)
            {
                //:         public 'column.TypeName' 'column.DisplayName' {get; set;}
                //:
            }
            //:    }
            //:
            //:}
        }
    }
}
