﻿namespace Aurum.Gen.Nodes
{
    public class ForEach : TemplateNode
    {
        public string Set {get; set;}
        public string Var { get; set; }
        public string Filter { get; set; }
    }
}
