﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen.Data
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
