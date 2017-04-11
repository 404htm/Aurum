using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core.CodeAnalysis
{
    /// <summary>
    /// Generates symbols from provided metadata
    /// </summary>
    public class CodeBuilder: ICodeBuilder
    {
        public void GenerateCallToGenericConstructor<T>()
        {
            throw new NotImplementedException();
            //var genericized = implementors.Select(i => i.Construct(targetType)).ToList();
           // return genericized.Select(t => $"new {t.ToDisplayString()}()").ToList(); //TODO: Convert to something less shaky
        }
    }
}
