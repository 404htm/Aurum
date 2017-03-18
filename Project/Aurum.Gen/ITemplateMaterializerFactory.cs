using System.Collections.Generic;

namespace Aurum.Gen
{
    public interface ITemplateMaterializerFactory
    {
        ITemplateMaterializer<T> Create<T>(IEnumerable<string> source);
    }
}