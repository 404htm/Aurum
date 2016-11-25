namespace Aurum.TemplateUtils
{
    public interface ITemplate<T>
    {
        string GetFileName(T model);
        void GenerateCode(T model, ICodeEmitter emitter);
    }
}

