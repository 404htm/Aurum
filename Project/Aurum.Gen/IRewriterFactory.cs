using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen
{
    public interface IRewriterFactory
    {
        ITemplateRewriter Create(Func<string, string> metaReplace, Func<string, string> inlineReplace);
    }
}
