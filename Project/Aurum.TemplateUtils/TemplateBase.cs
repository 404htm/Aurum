using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.TemplateUtils
{
    public abstract class TemplateBase<T> : ITemplate<T>
    {
        public abstract string GetFileName(T model);
        public abstract void GenerateCode(T model, ICodeEmitter emitter);
    }
}
