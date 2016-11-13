using System.Collections.Generic;

namespace Aurum.Core
{
    public interface IScope
    {
        object this[string key] { get;  set; }
        List<string> Keys { get; }
    }
}