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
        TextReader _inputFile;
        ITemplateRewriter _rewriter;

        /// <summary> Assembles a code writer from the input source, a rewriter, and a compiler - Should be created once per template file </summary>
        public TemplateMaterializer(TextReader inputFile, ITemplateRewriter rewriter)
        {
            _inputFile = inputFile;
            _rewriter = rewriter;
        }

        public void Build()
        {

        }

        public void Validate()
        {
            throw new NotImplementedException();
        }

        public ITemplate<T> Generate<T>(T model)
        {
            throw new NotImplementedException();
        }
    }
}
