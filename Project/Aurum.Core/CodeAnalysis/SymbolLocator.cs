using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core.CodeAnalysis
{
    /// <summary> Utility class to locate symbols in compilations.</summary>
    public static class SymbolLocator
    {
        /// <summary>
        /// Returns a list of classes that implement <typeparamref name="I"/>
        /// </summary>
        /// <typeparam name="I">Target interface</typeparam>
        /// <param name="compilation">Compilation</param>
        /// <returns>List of compilation classes implementing <typeparamref name="I"/></returns>
        public static List<INamedTypeSymbol> FindImplementations<I>(Compilation compilation)
        {
            //Cross reference the interface we want with the symbols in the compilation
            var targetInterface = compilation.GetTypeByMetadataName(typeof(I).FullName);
            if (targetInterface == null)
            {
                //TODO: Better exception type
                throw new ArgumentException("The specified interface cannot be located in compilation.", "<I>");
            }
            
            var global = compilation.Assembly.GlobalNamespace;
            var namespaces = global.ConstituentNamespaces;
            var directTypes = namespaces.SelectMany(m => m.GetTypeMembers());
            var allTypes = directTypes.SelectMany(locateTypesRecursively);
            var implementors = allTypes.Where(t => t.Interfaces.Contains(targetInterface)).ToList();

            return implementors;
        }

        public static List<INamedTypeSymbol> locateTypesRecursively(INamedTypeSymbol type)
        {
            var results = type.GetTypeMembers().SelectMany(locateTypesRecursively);
            return (new INamedTypeSymbol[] { type }.Concat(results)).ToList();
        }

    }
}
