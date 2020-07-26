using BaseMiCakeApplication.Domain.Aggregates;
using BaseMiCakeApplication.Domain.Repositories;
using BaseMiCakeApplication.Dto.InputDto.Account;
using MiCake.EntityFrameworkCore.Repository;
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

        public async Task<User> FindUserByName(string name)
        {
            return await DbSet.FirstOrDefaultAsync(s => s.Name.Equals(name));
        }

        public async Task<User> LoginAction(LoginUserInfo userDto)
        {
            return await DbSet.FirstOrDefaultAsync(s => (s.Name.Equals(userDto.Act) || s.Phone.Equals(userDto.Act) || s.Email.Equals(userDto.Act)) && s.Password.Equals(userDto.Pwd));
        }
    }
}
