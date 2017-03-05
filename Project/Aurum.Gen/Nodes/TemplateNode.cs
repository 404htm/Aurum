using System.Collections.Generic;

namespace Aurum.Gen.Nodes
{
    public abstract class TemplateNode
    {
        public TemplateNode()
        {
            Content = new List<TemplateNode>();
        }

        public List<TemplateNode> Content { get; set; }
    }
}
