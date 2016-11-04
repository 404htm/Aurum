using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen.Data
{
    public class ForEach : TemplateNode
    {
        public string Set {get; set;}
        public string Var { get; set; }
        public string Filter { get; set; }
    }
}
