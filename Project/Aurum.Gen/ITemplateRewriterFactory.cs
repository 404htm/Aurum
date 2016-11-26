using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen
{
    public interface ITemplateRewriterFactory
    {
        ITemplateRewriter Create(Func<string, string> metaReplace, Func<string, string> inlineReplace);
    }
}
