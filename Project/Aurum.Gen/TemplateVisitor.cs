using Aurum.Gen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen
{
    public class TemplateVisitor
    {
        ICodeMaterializer _materializer;
        Func<IScope, IScope> _scopeFactory;
        StringBuilder _sb = new StringBuilder();

        public TemplateVisitor(ICodeMaterializer codeMaterializer, Func<IScope,  IScope> scopeFactory)
        {
            _materializer = codeMaterializer;
            _scopeFactory = scopeFactory;
        }


        public void Visit(TemplateNode template, IScope scope = null)
        {
            //Resolve back to the correct subtype 
            Visit((dynamic)template, scope ?? _scopeFactory(null));
        }

        internal void Visit(Data.ForEach template, IScope scope)
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

        internal void Visit(Data.Code code, IScope scope)
        {
            _materializer.Process(scope, code);
        }
    }


}
