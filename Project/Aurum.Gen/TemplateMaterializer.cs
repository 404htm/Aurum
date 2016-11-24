using Aurum.Core.Parser;
using Aurum.TemplateUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen
{
    public class TemplateMaterializer
    {
        IEnumerable<string> _source;
        ITemplateRewriter _rewriter;
        IParserFactory _parserFactory;

        /// <summary> Assembles a code writer from the input source, a rewriter, and a compiler - Should be created once per template file </summary>
        public TemplateMaterializer(IEnumerable<string> source, ITemplateRewriter rewriter, IParserFactory parserFactory)
        {
            _source = source;
            _rewriter = rewriter;
            _parserFactory = parserFactory;
        }

        public async Task<TemplateBase<object>> Build()
        {
            var transformed = _rewriter.Rewrite(_source);

            var sb = new StringBuilder();
            foreach (var line in transformed) sb.AppendLine(line);

            var parser = _parserFactory.Create<TemplateBase<object>>();
            var template = await parser.Parse(sb.ToString());
            return template;
        }
    }
}
