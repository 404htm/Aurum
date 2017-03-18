using Aurum.Core.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen
{
    public class TemplateMaterializerFactory : ITemplateMaterializerFactory
    {
        ITemplateRewriterFactory _rewriter;
        IParserFactory _parser;

        public TemplateMaterializerFactory(ITemplateRewriterFactory rewriterFactory, IParserFactory parserFactory)
        {
            _rewriter = rewriterFactory;
            _parser = parserFactory;
        }

        public ITemplateMaterializer<T> Create<T>(IEnumerable<string> source)
        {
            return new TemplateMaterializer<T>(source, _rewriter, _parser);
        }
    }
}
