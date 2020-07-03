﻿using MiCake.Core.Modularity;
using MiCake.DDD.Extensions;
using MiCake.DDD.Extensions.Modules;
using MiCake.EntityFrameworkCore.Mapping;
using MiCake.EntityFrameworkCore.Repository;
using MiCake.EntityFrameworkCore.Repository.Freedom;
using MiCake.Mapster.Modules;
using MiCake.Uow.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace MiCake.EntityFrameworkCore.Modules
{
    [RelyOn(
        typeof(MiCakeMapsterModule),
        typeof(MiCakeUowModule),
        typeof(MiCakeDDDExtensionsModule))]
    public class MiCakeEFCoreModule : MiCakeModule
    {
        public override bool IsFrameworkLevel => true;

        public MiCakeEFCoreModule()
        {
        }

        public override void PreConfigServices(ModuleConfigServiceContext context)
        {
            var services = context.Services;
            //add ef repository provider
            services.AddScoped(typeof(IRepositoryProvider<,>), typeof(EFRepositoryProvider<,>));
            services.AddScoped(typeof(IFreeRepositoryProvider<,>), typeof(EFFreeRepositoryProvider<,>));

            services.AddTransient(typeof(EFCorePoManager<,>));

            //[Cancel:See Azure Board #ISSUE 12] add ef core interceptor
            //services.AddScoped(typeof(SaveChangesInterceptor));
        }

        public override void Initialization(ModuleBearingContext context)
        {
            // [Cancel:See Azure Board #ISSUE 12]
            // DiagnosticListener.AllListeners.Subscribe(new EfGlobalListener(context.ServiceProvider));
        }
    }
}
