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
    public class CodeEmitter : ICodeEmitter, ICodeSource
    {
        readonly int max_lines;
        int cur_line;
        public CodeEmitter(int maxLines = 1000)
        {
            max_lines = maxLines;
        }

        List<EmittedCodeLine> _lines = new List<EmittedCodeLine>();

        public void WriteLine(string text, [CallerLineNumber] int lineNumber = 0)
        {
            cur_line++;
            _lines.Add(new EmittedCodeLine { Text = text, InputLineNumber = lineNumber, OutputLineNumber = cur_line });
            if (cur_line > max_lines) throw new InvalidOperationException($"Code output has exceeded max lines ({max_lines}");
        }

        public List<EmittedCodeLine> GetCode()
        {
            return _lines;
        }
    }
}
