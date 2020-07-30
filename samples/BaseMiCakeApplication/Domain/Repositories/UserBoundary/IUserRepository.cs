using BaseMiCakeApplication.Domain.Aggregates.Account;
using BaseMiCakeApplication.Dto.InputDto.Account;
using MiCake.DDD.Domain;
using System;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Domain.Repositories.UserBoundary
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        public Task<User> FindUserByPhone(string phone);

        public Task<User> FindUserByName(string name);

        public Task AddUserAsync(User user);

        public Task<User> LoginAction(LoginUserInfo userDto);
    }
}
