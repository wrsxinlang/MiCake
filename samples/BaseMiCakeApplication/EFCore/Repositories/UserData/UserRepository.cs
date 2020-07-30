using BaseMiCakeApplication.Domain.Aggregates.Account;
using BaseMiCakeApplication.Domain.Repositories.UserBoundary;
using BaseMiCakeApplication.Dto.InputDto.Account;
using Mapster;
using MiCake.EntityFrameworkCore.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.EFCore.Repositories.UserData
{
    public class UserRepository : EFRepository<BaseAppDbContext, User, Guid>, IUserRepository
    {

        public UserRepository(IServiceProvider serviceProvider):base(serviceProvider)
        {
        }

        public async Task<User> FindUserByPhone(string phone)
        {

            var persistentObject = await DbSet.FirstOrDefaultAsync(s => s.Phone.Equals(phone));
            return persistentObject.Adapt<User>();
        }

        public async Task<User> FindUserByName(string name)
        {
            var persistentObject = await DbSet.FirstOrDefaultAsync(s => s.Name.Equals(name));
            return persistentObject.Adapt<User>();
        }

        public Task AddUserAsync(User user)
        {
            DbSet.Add(user);
            return Task.CompletedTask;
        }

        public async Task<User> LoginAction(LoginUserInfo userDto)
        {
            var persistentObject = await DbSet.FirstOrDefaultAsync(s => (s.Name.Equals(userDto.Act) ||s.Phone.Equals(userDto.Act)||s.Email.Equals(userDto.Act))&&s.Password.Equals(userDto.Pwd));
            return persistentObject.Adapt<User>();
        }
    }
}
