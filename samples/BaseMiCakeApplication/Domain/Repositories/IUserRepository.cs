using BaseMiCakeApplication.Domain.Aggregates;
using BaseMiCakeApplication.Dto.InputDto.Account;
using MiCake.DDD.Domain;
using System;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Domain.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        public Task<User> FindUserByPhone(string phone);

        public Task<User> FindUserByName(string name);

        public Task AddUserAsync(User user);

        public Task<User> LoginAction(LoginUserInfo userDto);
    }
}
