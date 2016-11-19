namespace Aurum.TemplateUtils
{
    public interface ITemplate<T>
    {
        string Source { get; }
        string FileName { get; }
        void Body(T data);
    }
}

