using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aurum.TemplateUtils
{
    public interface ITemplate<T>
    {
        string Source { get; }
        string FileName { get; }
        void Body(T data);
    }
}
