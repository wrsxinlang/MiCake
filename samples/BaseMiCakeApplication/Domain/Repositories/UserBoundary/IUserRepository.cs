using BaseMiCakeApplication.Domain.Aggregates;
using BaseMiCakeApplication.Dto.InputDto.Account;
using BaseMiCakeApplication.Models;
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
