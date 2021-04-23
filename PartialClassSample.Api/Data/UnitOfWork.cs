using BaseEntity.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace PartialClassSample.Api.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        DbContext Context { get; }

        public UnitOfWork(UserContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CommitAsync() => await Context.SaveChangesAsync() > 0;
    }
}
