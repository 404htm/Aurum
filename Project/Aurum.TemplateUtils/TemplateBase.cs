using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.TemplateUtils
{
    public abstract class TemplateBase<T> : ITemplate<T>
    {
        protected Action<string> _emitter;

        public abstract void Body(T model);
        public abstract string GetFileName(T model);

        public void RegisterCodeEmitter(Action<string> codeEmitter)
        {
            _emitter = codeEmitter;
        }
    }
}
