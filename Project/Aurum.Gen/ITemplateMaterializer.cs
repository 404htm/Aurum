using System.Threading.Tasks;
using Aurum.TemplateUtils;

namespace Aurum.Gen
{
    public interface ITemplateMaterializer<TModel>
    {
        Task<ITemplate<TModel>> Build();
    }
}