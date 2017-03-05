using System.Collections.Generic;

namespace Aurum.Gen.Nodes
{
    public class If : TemplateNode
    {
        public string Condition { get; set; }
        public List<TemplateNode> Else { get; set; }
    }
}
