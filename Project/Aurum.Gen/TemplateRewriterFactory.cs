using System;

namespace Aurum.Gen
{
    public class TemplateRewriterFactory : ITemplateRewriterFactory
    {
        string _metaSymbol;
        string _inlineSymbolL;
        string _inlineSymbolR;

        public TemplateRewriterFactory(string metaSymbol, string inlineSymbolL, string inlineSymbolR)
        {
            _metaSymbol = metaSymbol;
            _inlineSymbolL = inlineSymbolL;
            _inlineSymbolR = inlineSymbolR;
        }

        public ITemplateRewriter Create(Func<string, string> metaReplace, Func<string, string> inlineReplace)
        {
            return new TemplateRewriter(_metaSymbol, _inlineSymbolL, _inlineSymbolR, metaReplace, inlineReplace);
        }
    }
}
