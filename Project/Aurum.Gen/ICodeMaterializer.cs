using Aurum.Gen.Data;

namespace Aurum.Gen
{
    public interface ICodeMaterializer
    {
        void Process(IScope scope, Code code);
    }
}