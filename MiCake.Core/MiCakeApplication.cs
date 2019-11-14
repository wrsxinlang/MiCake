﻿using MiCake.Core.Abstractions;
using MiCake.Core.Abstractions.Builder;
using MiCake.Core.Abstractions.DependencyInjection;
using MiCake.Core.Abstractions.ExceptionHandling;
using MiCake.Core.Abstractions.Logging;
using MiCake.Core.Abstractions.Modularity;
using MiCake.Core.Builder;
using MiCake.Core.DependencyInjection;
using MiCake.Core.ExceptionHandling;
using MiCake.Core.Logging;
using MiCake.Core.Modularity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MiCake.Core
{
    public class MiCakeApplication : IMiCakeApplication
    {
        public Type StartUpType { get; }

        public IMiCakeBuilder Builder { get; }

        public IServiceProvider ServiceProvider { get; private set; }

        public IServiceCollection Services { get; private set; }

        private Type _startUp;

        public MiCakeApplication(Type startUp, IServiceCollection services)
        {
            if (startUp == null)
                throw new ArgumentException("Please add startUp type when you use AddMiCake().");

            _startUp = startUp;
            Services = services;

            var micakeModuleManager = new MiCakeModuleManager();
            micakeModuleManager.PopulateDefaultModule(_startUp);
            micakeModuleManager.ActivateServices(services, s =>
            {
                //auto activate has micake support services
                var diManager = new DefaultMiCakeDIManager(services);
                diManager.PopulateAutoService(s);
            });

            Builder = new MiCakeBuilder(services, micakeModuleManager);

            services.AddSingleton<IMiCakeModuleManager>(micakeModuleManager);
            services.AddSingleton<IMiCakeBuilder>(Builder);

            //add micake core serivces
            AddMiCakeCoreSerivces(services);
        }

        public virtual IMiCakeApplication Configure(Action<IMiCakeBuilder> builderConfigAction)
        {
            if (Builder == null)
                throw new ArgumentException("MiCake Application is not activation.Please use services.AddMiCake() in your Startup.cs.");

            builderConfigAction(Builder);

            return this;
        }

        public virtual void Init()
        {
            if (ServiceProvider == null)
                throw new ArgumentNullException(nameof(ServiceProvider));

            using var scpoe = ServiceProvider.CreateScope();
            var moduleBoot = (IMiCakeModuleBoot)scpoe.ServiceProvider.GetRequiredService<IMiCakeModuleBoot>();
            moduleBoot.Initialization(new ModuleBearingContext(ServiceProvider, Builder.ModuleManager.miCakeModules));
        }

        public virtual void ShutDown(Action<ModuleBearingContext> shutdownAction = null)
        {
            using var scpoe = ServiceProvider.CreateScope();
            var moduleBoot = (IMiCakeModuleBoot)scpoe.ServiceProvider.GetRequiredService<IMiCakeModuleBoot>();
            var context = new ModuleBearingContext(ServiceProvider, Builder.ModuleManager.miCakeModules);
            shutdownAction?.Invoke(context);
            moduleBoot.ShutDown(context);
        }

        public virtual void Dispose()
        {
        }

        protected virtual IMiCakeApplication SetServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            return this;
        }


        private void AddMiCakeCoreSerivces(IServiceCollection services)
        {
            services.AddSingleton<IMiCakeModuleBoot, MiCakeModuleBoot>();
            services.AddSingleton<IDIContainer>(new DefaultDIContainer(services));
            services.AddSingleton<IMiCakeErrorHandler, DefaultMiCakeErrorHandler>();
            services.AddSingleton<ILogErrorHandlerProvider, DefaultLogErrorHandlerProvider>();
        }
    }
}
