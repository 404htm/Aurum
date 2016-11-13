﻿using Aurum.Core;
using Aurum.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Aurum.Core
{
    /// <summary>Variable storage object enabling nested scoping rules</summary>
    public class Scope : IScope
    {
        IScope _outer;
        Dictionary<string, object> _vars { get; set; }

        public Scope(IScope outer = null)
        {
            _outer = outer;
            _vars = new Dictionary<string, object>();
        }

        public object this[string key]
        {
            get { return _vars.SafeGet(key) ?? _outer?[key] ?? null; }
            set { _vars[key] = value; }
        }

        public List<string> Keys => _vars.Keys.Union(_outer?.Keys??Enumerable.Empty<string>()).ToList();

    }
}
