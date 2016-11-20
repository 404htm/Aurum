using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aurum.Gen
{
    /// <summary>
    /// This class does an intermediary tranform on Aurum templates...
    /// Instead of explicitly parsing the template it substitutes the meta sections with code that writes to an emitter
    /// That code can then run using the c# parser and the output can be used to build the resulting code
    /// </summary>
    public class TemplateRewriter
    {
        readonly Func<string, string> _substitution;
        readonly Regex _lineFinder;


        public TemplateRewriter(string metaLineSymbol, string metaEscapeSymbol, Func<string, string> substitution)
        {
            var line = Regex.Escape(metaLineSymbol);
            var esc = Regex.Escape(metaEscapeSymbol);

            _lineFinder = new Regex($@"(?<=^\s*({line}|//{line}))(.*)$");
            _substitution = substitution;
        }

        public IEnumerable<string> Rewrite(IEnumerable<string> template)
        {
            foreach (var line in template)
            {
                var result = _lineFinder.Match(line);
                if (result.Success) yield return _substitution(result.Value);
                else yield return line;
            }
        }



    }
}
