using System.Collections.Generic;

namespace Aurum.Gen
{
    public interface ITemplateRewriter
    {
        IEnumerable<string> Rewrite(IEnumerable<string> template);
    }
}