using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Aurum.Gen.Validators
{
    /// <summary>
    /// Validator for C# code, returns syntax based code diagnostics, optionally performs full compilation
    /// Create per class and reuse for best efficiency
    /// </summary>
    public class CSharpValidator : ICodeValidator
    {
        bool _compile;
        Lazy<Compilation> _compilation;

        public CSharpValidator(bool compile = false)
        {
            _compile = compile;
            if (compile)
            {
                //_compilation = new Lazy<Compilation> (() =>
                //    {
                //        var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
                //        return CSharpCompilation.Create(Guid.NewGuid().ToString(), null, null, options);
                //    }, true);
            }
            //TODO: Usings
            //TODO: Cancellation
        }

        public List<ValidationResult> Parse(string code)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
            var diagnostics = tree.GetDiagnostics();

            if (diagnostics.Any()) return diagnostics.Select(ToValidationResult).ToList();
            else return (_compile)?Compile(tree):new List<ValidationResult>();
        }

        private List<ValidationResult> Compile(SyntaxTree tree)
        {
            throw new NotImplementedException("Compile is not fully implemented");
            //Compilation com = _compilation.Value.AddSyntaxTrees(tree);
            //return com.GetParseDiagnostics().Select(ToValidationResult).ToList();

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
