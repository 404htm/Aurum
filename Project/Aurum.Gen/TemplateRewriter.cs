﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aurum.Gen
{
    /// <summary>
    /// This class does an intermediary tranform on Aurum templates...
    /// Instead of explicitly parsing the template it substitutes the meta sections with code that writes to an emitter
    /// The code generated by this class can be compiled into something that will emit the final code when run
    /// </summary>
    public class TemplateRewriter : ITemplateRewriter
    {
        readonly Func<string, string> _metaReplacement;
        readonly Func<string, string> _inlineReplacement;
        readonly List<string> _inlineSymbols;
        readonly Regex _metaFinder;
        readonly Regex _inlineFinder;

        public TemplateRewriter(string metaSymbol, string inlineSymbolL, string inlineSymbolR, Func<string, string> metaReplace, Func<string, string> inlineReplace)
        {
            var line = Regex.Escape(metaSymbol);
            var escL = Regex.Escape(inlineSymbolL);
            var escR = Regex.Escape(inlineSymbolR);

            _inlineSymbols = new List<string> { inlineSymbolL, inlineSymbolR };
            _metaFinder = new Regex($@"(?<=^\s*({line}|//{line}))(.*)$");
            _inlineFinder = new Regex($@"(?<!\\){escL}(?<esc>.+?)(?<!\\){escR}");

            _metaReplacement = metaReplace;
            _inlineReplacement = inlineReplace;
        }

        /// <summary> Detects metacode and replaces with specified transforms </summary>
        public IEnumerable<string> Rewrite(IEnumerable<string> template)
        {
            foreach (var line in template) yield return EscapeLineIfMeta(line);
        }

        /// <summary>Checks for meta lines and applies specified transformation</summary>
        private string EscapeLineIfMeta(string line)
        {
            var metaMatch = _metaFinder.Match(line);
            if (metaMatch.Success)
            {
                var result = _metaReplacement(metaMatch.Value);
                result = RaiseInlineStatements(result);
                result = RestoreEscapedCharacters(result);
                result = RestoreEscapedCharacters(result);

                return result;
            }
            else
            {
                return line;
            }
        }

        /// <summary>Checks for inline statements and replaces with specified transformation. Should only be called on meta lines</summary>
        private string RaiseInlineStatements(string line)
        {
            return _inlineFinder.Replace(line, _inlineReplacement("${esc}"));
        }

        private string RestoreEscapedCharacters(string line)
        {
            var result = line;
            foreach (var s in _inlineSymbols) result = result.Replace($@"\{s}", s);
            return result;
        }

    }
}
