using System.Collections.Generic;

namespace Aurum.Gen
{
     public interface ICodeSource
    {
        List<EmittedCodeLine> GetCode();
    }
}