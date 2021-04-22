using Messages.Core;
using Microsoft.EntityFrameworkCore;
using PartialClassSample.Api.Models;
using PartialClassSample.Api.Models.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartialClassSample.Api.Data
{
    public class RegisterRepository : IRegisterRepository
    {
        protected DbContext Context { get; }

        public DbSet<Register> DbSet => Context.Set<Register>();

        public RegisterRepository(UserContext context) => Context = context;

        public Task<List<Register>> GetAllAsync()
            => DbSet.ToListAsync();

        public async Task<Maybe<Register>> FindAsync(int id)
            => await DbSet.SingleOrDefaultAsync(register => register.Id == id);

        public Task AddAsync(Register register)
        {
            DbSet.Add(register);
            return Task.CompletedTask;
        }
    }
}
