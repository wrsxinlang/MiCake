﻿using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace MiCake.Core.Modularity
{
    public abstract class MiCakeModule : IMiCakeModule, IModuleConfigServicesLifeTime, IModuleLifeTime
    {
        /// <summary>
        /// Tag this module is farmework level.
        /// Framework level modules do not need to be traversed.
        /// </summary>
        public virtual bool IsFrameworkLevel => false;

        /// <summary>
        /// Auto register service to dependency injection framework.
        /// </summary>
        public virtual bool IsAutoRegisterServices => true;

        public MiCakeModule()
        {
        }

        public virtual void ConfigServices(ModuleConfigServiceContext context)
        {
        }

        public virtual void Initialization(ModuleBearingContext context)
        {
        }

        public virtual void PostConfigServices(ModuleConfigServiceContext context)
        {
        }

        public virtual void PostInitialization(ModuleBearingContext context)
        {
        }

        public virtual void PreConfigServices(ModuleConfigServiceContext context)
        {
        }

        public virtual void PreInitialization(ModuleBearingContext context)
        {
        }

        public virtual void PreShutDown(ModuleBearingContext context)
        {
        }

        public virtual void Shutdown(ModuleBearingContext context)
        {
        }

        protected static T GetServiceFromCollection<T>(IServiceCollection services)
        {
            return (T)services
                .LastOrDefault(d => d.ServiceType == typeof(T))
                ?.ImplementationInstance;
        }
    }
}
