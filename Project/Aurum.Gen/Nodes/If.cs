using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen.Nodes
{
    public class If : TemplateNode
    {
        public string Condition { get; set; }
        public List<TemplateNode> Else { get; set; }
    }
}
