using System.Linq;

namespace Aurum.Core.Parser
{
    public class ParserFactory : IParserFactory
    {
        public IExpressionParser<T> Create<T>()
        {
            var result = new ExpressionParser<T>();
            result.Register(typeof(Queryable));
            return result;
        }
    }
}
