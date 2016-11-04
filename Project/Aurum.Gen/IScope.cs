using System.Collections.Generic;

namespace Aurum.Gen
{
    public interface IScope
    {
        T Get<T>(string name) where T : class;
        void Set<T>(string name, T value) where T : class;

        IEnumerable<T> GetList<T>(string name) where T : class;
        void SetList<T>(string name, IEnumerable<T> value) where T : class;
    }
}