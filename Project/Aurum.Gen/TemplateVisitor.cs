﻿using Aurum.Core.Parser;
using Aurum.Gen.Nodes;
using System;
using System.Text;

namespace Aurum.Gen
{
    public class TemplateVisitor
    {
        ICodeMaterializer _materializer;
        IParserFactory _parserFactory;
        Func<IScope, IScope> _scopeFactory;
        StringBuilder _sb = new StringBuilder();

        public TemplateVisitor(ICodeMaterializer codeMaterializer, Func<IScope,  IScope> scopeFactory, IParserFactory parserFactory)
        {
            _materializer = codeMaterializer;
            _scopeFactory = scopeFactory;
            _parserFactory = parserFactory;
        }


        public void Visit(TemplateNode template, IScope scope = null)
        {
            //Resolve back to the correct subtype 
            Build((dynamic)template, scope ?? _scopeFactory(null));
        }

        internal void Build(Code code, IScope scope)
        {
            _materializer.Process(scope, code);
        }

        internal void Build(ForEach template, IScope scope)
        {
            var set = template.Set;
            var items = scope.GetList<object>(set);

            foreach(var item in items)
            {
                var inner = _scopeFactory(scope);
                inner.Set(template.Var, item);
                foreach (var c in template.Content) Visit(c, inner);
            }
        }

        internal void Build(If template, IScope scope)
        {

        }

    }


}
