using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Aurum.Gen.Validators
{
    public class CSharpValidator : ICodeValidator
    {
        public List<ValidationResult> Messages { get; private set; }

        public CSharpValidator()
        {
            //TODO: Usings
            //TODO: Compilation vs Syntax Parsing
            //TODO: Cancellation
        }

        public List<ValidationResult> Parse(string code)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
            var diagnostics = tree.GetDiagnostics();
            return diagnostics.Cast<Diagnostic>().Select(ToValidationResult).ToList();
        }

        private ValidationResult ToValidationResult(Diagnostic diagnostic)
        {
            return new Validators.ValidationResult
            {
                Code = diagnostic.Id,
                Message = diagnostic.GetMessage(),
                Start = diagnostic.Location.SourceSpan.Start,
                Length = diagnostic.Location.SourceSpan.Length
            };
        }

    }
}
