using BaseMiCakeApplication.Controllers.Comman;
using BaseMiCakeApplication.Domain.Aggregates.Idea;
using BaseMiCakeApplication.Domain.Repositories.NewIdeaBoundary;
using BaseMiCakeApplication.Infrastructure.StorageModels;
using MiCake.EntityFrameworkCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.EFCore.Repositories.NewIdeaBoundry
{
    public class CommentDatasRepository : EFRepositoryWithPO<BaseAppDbContext, CommentDatas, CommentDatasSnapshotModel, Guid>, ICommentDatasRepository
    {
        public CommentDatasRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        /// <summary>
        /// 获取评论
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="classObjectID"></param>
        /// <returns></returns>
        public List<CommentDatas> GetCommentsDatas(int enumType, Guid classObjectID)
        {
            var persistentObjects = DbSet.Where(x => x.ReplayType == (CY_Classify)enumType && x.RelationObjectID == classObjectID);
            if (persistentObjects.Count() == 0) return new List<CommentDatas>();

            return MapToDO(persistentObjects).ToList();
        }
    }
}
