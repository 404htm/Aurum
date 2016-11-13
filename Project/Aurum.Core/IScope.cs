using System.Collections.Generic;

namespace Aurum.Core
{
    public interface IScope
    {
        T Get<T>(string name);
        void Set<T>(string name, T value);
        List<string> Keys { get; }
    }
}