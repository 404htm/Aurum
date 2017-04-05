namespace Aurum.Core.Parser
{
    public interface IParserFactory
    {
        IParser<T> Create<T>();
    }
}
