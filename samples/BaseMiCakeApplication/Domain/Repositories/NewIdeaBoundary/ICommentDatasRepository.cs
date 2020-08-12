using BaseMiCakeApplication.Domain.Aggregates.Idea;
using MiCake.DDD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Domain.Repositories.NewIdeaBoundary
{
    public interface ICommentDatasRepository : IRepository<CommentDatas, Guid>
    {
        /// <summary>
        /// 获取评论
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="classObjectID"></param>
        /// <returns></returns>
        List<CommentDatas> GetCommentsDatas(int enumType,Guid classObjectID);
    }
}
