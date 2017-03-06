using Aurum.TemplateUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Aurum.Gen
{
    /// <summary>
    /// Minimal code builder implementation - will likely be replaced once requirements are determined
    /// </summary>
    public class BasicEmitter : ICodeEmitter
    {
        StringBuilder _builder = new StringBuilder();

        public void WriteLine(string text, [CallerLineNumber] int lineNumber = 0)
        {
            _builder.AppendLine(text);
        }

        public string GetCode()
        {
            return _builder.ToString();
        }
    }
}
