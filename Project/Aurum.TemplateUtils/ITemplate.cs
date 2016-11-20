namespace Aurum.TemplateUtils
{
    public interface ITemplate<T>
    {
        string GetFileName(T model);
        void Body(T model);
    }
}

