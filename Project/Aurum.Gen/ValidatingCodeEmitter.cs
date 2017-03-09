using Aurum.TemplateUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Aurum.Gen.Validators;

namespace Aurum.Gen
{
    public class ValidatingCodeEmitter : ICodeEmitter
    {
        ICodeValidator _validator;
        public ValidatingCodeEmitter(ICodeValidator validator)
        {

        }

        public void WriteLine(string text, [CallerLineNumber] int lineNumber = 0)
        {
            throw new NotImplementedException();
        }
    }
}
