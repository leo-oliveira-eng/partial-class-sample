using System.Threading.Tasks;

namespace PartialClassSample.Api.Models.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
