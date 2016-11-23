using Aurum.Core;
using Aurum.Gen.Nodes;

namespace Aurum.Gen
{
    public interface ICodeMaterializer
    {
        void Process(IScope scope, Code code);
    }
}