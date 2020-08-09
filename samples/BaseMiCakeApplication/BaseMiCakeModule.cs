using BaseMiCakeApplication.Domain.Repositories;
using BaseMiCakeApplication.Domain.Repositories.NewIdeaBoundary;
using BaseMiCakeApplication.Domain.Repositories.UserBoundary;
using BaseMiCakeApplication.EFCore.Repositories;
using BaseMiCakeApplication.EFCore.Repositories.NewIdeaBoundry;
using BaseMiCakeApplication.EFCore.Repositories.UserData;
using MiCake;
using MiCake.Core.Modularity;

namespace BaseMiCakeApplication
{
    public class BaseMiCakeModule : MiCakeModule
    {
        public override void ConfigServices(ModuleConfigServiceContext context)
        {
            context.RegisterRepository<IItineraryRepository, ItineraryRepository>();
            context.RegisterRepository<IUserRepository, UserRepository>();
            context.RegisterRepository<INewIdeaRepository, NewIdeaRepository>();
        }
    }
}
