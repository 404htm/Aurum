using Aurum.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Aurum.Core
{
    public class Scope : IScope
    {
        IScope _parent;
        Dictionary<string, object> _vars { get; set; }
        Dictionary<string, IEnumerable<object>> _sets { get; set; }

        public Scope(IScope parent = null)
        {
            _parent = parent;
            _vars = new Dictionary<string, object>();
            _sets = new Dictionary<string, IEnumerable<object>>();
        }

        public T Get<T>(string name)
        {
            var result = (T)(_vars.SafeGet(name));

            if (result != null) return result;
            else if (_parent != null) return _parent.Get<T>(name);
            else return default(T);
        }
        public void Set<T>(string name, T value) => _vars[name] = _vars[name] = value;

        public IEnumerable<T> GetList<T>(string name) => _sets.SafeGet(name)?.Cast<T>() ?? _parent?.GetList<T>(name);
        public void SetList<T>(string name, IEnumerable<T> value) => _sets[name] = (IEnumerable<object>)value;
    }
}
