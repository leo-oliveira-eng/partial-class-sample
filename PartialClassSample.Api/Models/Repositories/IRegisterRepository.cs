using Messages.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartialClassSample.Api.Models.Repositories
{
    public interface IRegisterRepository
    {
        Task<List<Register>> GetAllAsync();

        Task<Maybe<Register>> FindAsync(string email);

        Task AddAsync(Register register);
    }
}
