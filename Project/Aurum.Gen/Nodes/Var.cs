using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen.Nodes
{
    public class Var : TemplateNode
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
