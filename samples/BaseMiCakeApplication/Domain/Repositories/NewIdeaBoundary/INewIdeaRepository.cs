using MiCake.DDD.Domain;
using BaseMiCakeApplication.Domain.Aggregates.Idea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseMiCakeApplication.Domain.Repositories.NewIdeaBoundary
{
    public interface INewIdeaRepository : IRepository<NewIdea, Guid>
    {
        List<NewIdea> GetList(string searchKey,int page,int limit,string orderType, out int total);
    }
}
