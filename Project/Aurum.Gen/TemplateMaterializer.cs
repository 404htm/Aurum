using Aurum.Core.Parser;
using Aurum.TemplateUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen
{
    public class TemplateMaterializer<TModel> : ITemplateMaterializer<TModel>
    {
        IEnumerable<string> _source;
        ITemplateRewriterFactory _rewriterFactory;
        IParserFactory _parserFactory;

        /// <summary> Assembles a code writer from the input source, a rewriter, and a compiler - Should be created once per template file </summary>
        public TemplateMaterializer(IEnumerable<string> source, ITemplateRewriterFactory rewriterFactory, IParserFactory parserFactory)
        {
            _source = source;
            _rewriterFactory = rewriterFactory;
            _parserFactory = parserFactory;
        }

        public async Task<ITemplate<TModel>> Build()
        {
            //TODO: Extract real emitter name
            var emitterName = "emitter";
            var methodName = nameof(ICodeEmitter.WriteLine);

            Func<string, string> metaFunc = (line) => $@"{emitterName}.{methodName}(""{line}"");";
            Func<string, string> inlineFunc = (text) => $@"{{{text}}}";

            var rewriter = _rewriterFactory.Create(metaFunc, inlineFunc);
            var transformed = rewriter.Rewrite(_source);

            var sb = new StringBuilder();
            foreach (var line in transformed) sb.AppendLine(line);

            var parser = _parserFactory.Create<ITemplate<TModel>>();
            return await parser.Parse(sb.ToString());
        }
    }
}
