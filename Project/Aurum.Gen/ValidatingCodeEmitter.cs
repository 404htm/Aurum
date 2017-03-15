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
    /// <summary>
    /// Builds code output line by line and provides diagnostics correlating errors in source and output
    /// </summary>
    public class ValidatingCodeEmitter : ICodeEmitter
    {
        ICodeValidator _validator;
        public ValidatingCodeEmitter(ICodeValidator validator)
        {
            _validator = validator;
        }

        public void WriteLine(string text, [CallerLineNumber] int lineNumber = 0)
        {
            throw new NotImplementedException();
        }

        public List<ValidationResult> GetDiagnostics()
        {
            throw new NotImplementedException();
        }
    }
}
