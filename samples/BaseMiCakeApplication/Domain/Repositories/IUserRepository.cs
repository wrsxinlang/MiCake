using BaseMiCakeApplication.Domain.Aggregates;
using MiCake.DDD.Domain;
using System;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Domain.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        public Task<User> FindUserByPhone(string phone);

        public Task AddUserAsync(User user);
    }
}
