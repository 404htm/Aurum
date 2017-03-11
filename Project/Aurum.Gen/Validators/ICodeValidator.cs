using System.Collections.Generic;

namespace Aurum.Gen.Validators
{
    public interface ICodeValidator
    {
        List<ValidationResult> Parse(string code);


    }
}