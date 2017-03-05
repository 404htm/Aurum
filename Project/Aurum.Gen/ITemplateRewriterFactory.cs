using System;

namespace Aurum.Gen
{
    public interface ITemplateRewriterFactory
    {
        ITemplateRewriter Create(Func<string, string> metaReplace, Func<string, string> inlineReplace);
    }
}
