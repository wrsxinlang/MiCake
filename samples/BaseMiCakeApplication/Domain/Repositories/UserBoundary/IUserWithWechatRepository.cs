
using MiCake.DDD.Domain;
using System;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Domain.Repositories.UserBoundary
{
    public interface IUserWithWechatRepository : IRepository<UserWithWechat, long>
    {
        public Task<Guid> GetUserIdWithOpenId(string OpenId);
    }
}
