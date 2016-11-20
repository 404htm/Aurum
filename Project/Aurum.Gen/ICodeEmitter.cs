using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen
{
    public interface ICodeEmitter
    {
        void Emit(string code);
    }
}
