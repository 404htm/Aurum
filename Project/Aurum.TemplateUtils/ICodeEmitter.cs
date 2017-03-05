using System.Runtime.CompilerServices;

namespace Aurum.TemplateUtils
{
    public interface ICodeEmitter
    {
        void WriteLine(string text, [CallerLineNumber] int lineNumber = 0);
    }
}
