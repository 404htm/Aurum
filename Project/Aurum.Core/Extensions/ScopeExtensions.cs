using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace Aurum.Core.Extensions
{
    public static class ScopeExtensions
    {
        /// <summary  an IScope object to an expando object for used by other components </summary>
        public static ExpandoObject ToExpando(this IScope scope)
        {
            var obj = new ExpandoObject();
            var lookup = obj as IDictionary<string, object>;

            foreach(var key in scope.Keys) lookup[key] = scope[key];

            return obj;
        }
    }
}
