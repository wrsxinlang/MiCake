using BaseMiCakeApplication.Domain.Aggregates.Idea;
using BaseMiCakeApplication.Domain.Repositories.NewIdeaBoundary;
using BaseMiCakeApplication.Infrastructure.StorageModels;
using MiCake.EntityFrameworkCore.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.EFCore.Repositories.NewIdeaBoundry
{
    public class NewIdeaRepository : EFRepositoryWithPO<BaseAppDbContext, NewIdea, NewIdeaSnapshotModel, Guid>, INewIdeaRepository
    {
        public NewIdeaRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
        public List<NewIdea> GetList(string searchKey, int page, int limit, string orderType, out int total)
        {
            var persistentObjects = DbSet.Where(s => !s.IsDeleted);
            if (!string.IsNullOrEmpty(searchKey)) persistentObjects = persistentObjects.Where(x => x.Title.Contains(searchKey));
            switch (orderType)
            {

                default:
                    persistentObjects.OrderByDescending(x => x.CreationTime);
                    break;
            }
            total = persistentObjects.Count();
            if (page > 0 && limit > 0)
            {
                persistentObjects = persistentObjects.Skip((page - 1) * limit).Take(limit);
            }

            return MapToDO(persistentObjects).ToList();
        }
    }
}
