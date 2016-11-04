using Aurum.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Aurum.Gen
{
    public class Scope : IScope
    {
        Scope _parent;
        Dictionary<string, object> _vars { get; set; }
        Dictionary<string, IEnumerable<object>> _sets { get; set; }

        public Scope(Scope parent = null)
        {
            _parent = parent;
            _vars = new Dictionary<string, object>();
            _sets = new Dictionary<string, IEnumerable<object>>();
        }
        
        public T Get<T>(string name) where T : class => (T)(_vars.SafeGet(name) ??(_parent?.Get<T>(name)));
        public void Set<T>(string name, T value) where T : class => _vars[name] = _vars[name] = value;

        public IEnumerable<T> GetList<T>(string name) where T : class => _sets.SafeGet(name)?.Cast<T>() ?? _parent?.GetList<T>(name);
        public void SetList<T>(string name, IEnumerable<T> value) where T : class => _sets[name] = _sets[name] = value;
    }
}
