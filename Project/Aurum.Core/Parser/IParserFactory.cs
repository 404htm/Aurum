namespace Aurum.Core.Parser
{
    public interface IParserFactory
    {
        IExpressionParser<T> Create<T>();
    }
}
