using Aurum.TemplateUtils;
using Aurum.Gen.Tests.Metadata;

namespace Aurum.Gen.Tests.Templates
{

    public class basic : ITemplate<BasicTable>
    {
        public string GetFileName(BasicTable data)
        {
            return $"{data.Name}.cs";
        }

        public void GenerateCode(BasicTable data, ICodeEmitter emitter)
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
