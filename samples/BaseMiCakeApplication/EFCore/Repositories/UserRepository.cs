using BaseMiCakeApplication.Domain.Aggregates;
using BaseMiCakeApplication.Domain.Repositories;
using MiCake.EntityFrameworkCore.Repository;
using MiCake.EntityFrameworkCore.Uow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.EFCore.Repositories
{
    public class UserRepository : EFRepository<BaseAppDbContext, User, Guid>, IUserRepository
    {
        public UserRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<User> FindUserByPhone(string phone)
        {
            return await DbSet.FirstOrDefaultAsync(s => s.Phone.Equals(phone));
        }

        public Task AddUserAsync(User user)
        {
            DbSet.Add(user);
            return Task.CompletedTask;
        }
    }
}
