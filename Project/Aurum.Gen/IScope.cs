using System.Collections.Generic;

namespace Aurum.Gen
{
    public interface IScope
    {
        T Get<T>(string name);
        void Set<T>(string name, T value);

        IEnumerable<T> GetList<T>(string name);
        void SetList<T>(string name, IEnumerable<T> value);
    }
}