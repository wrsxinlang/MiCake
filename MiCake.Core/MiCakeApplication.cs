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

        private IServiceProvider _serviceProvider;
        private Type _startUp;

        public MiCakeApplication(
            Type startUp,
            IServiceCollection services,
            Action<IMiCakeBuilder> builderConfigAction)
        {
            if (startUp == null)
                throw new ArgumentException("Please add startUp type when you use AddMiCake().");

            _startUp = startUp;

            //add micake core serivces
            AddMiCakeCoreSerivces(services);

            Builder = new MiCakeBuilder(services, new MiCakeModuleManager());
            builderConfigAction?.Invoke(Builder);
            //populate normal and feature modules
            ((MiCakeModuleManager)Builder.ModuleManager).PopulateModules(_startUp);
            ((MiCakeModuleManager)Builder.ModuleManager).ConfigServices(services, s =>
            {
                //auto activate has micake support services
                var diManager = new DefaultMiCakeDIManager(services);
                diManager.PopulateAutoService(s);
            });
            services.AddSingleton(Builder);
        }

        public virtual void Init()
        {
            if (_serviceProvider == null)
                throw new ArgumentNullException(nameof(_serviceProvider));

            using var scpoe = _serviceProvider.CreateScope();
            //active service locator
            scpoe.ServiceProvider.GetRequiredService<IServiceLocator>();

            var moduleBoot = scpoe.ServiceProvider.GetRequiredService<IMiCakeModuleBoot>();
            moduleBoot.Initialization(new ModuleBearingContext(_serviceProvider, Builder.ModuleManager.MiCakeModules));
        }

        public virtual void ShutDown(Action<ModuleBearingContext> shutdownAction = null)
        {
            if (_serviceProvider == null)
                throw new ArgumentNullException(nameof(ServiceProvider));

            using var scpoe = _serviceProvider.CreateScope();
            var moduleBoot = scpoe.ServiceProvider.GetRequiredService<IMiCakeModuleBoot>();
            var context = new ModuleBearingContext(_serviceProvider, Builder.ModuleManager.MiCakeModules);
            shutdownAction?.Invoke(context);
            moduleBoot.ShutDown(context);
        }

        public virtual void Dispose()
        {
        }

        protected virtual IMiCakeApplication SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            return this;
        }

        private void AddMiCakeCoreSerivces(IServiceCollection services)
        {
            services.AddSingleton<IMiCakeModuleBoot, MiCakeModuleBoot>();
            services.AddSingleton<IServiceLocator, ServiceLocator>(provider =>
            {
                ServiceLocator.Instance.Locator = provider;
                return ServiceLocator.Instance;
            });
            services.AddSingleton<IMiCakeErrorHandler, DefaultMiCakeErrorHandler>();
            services.AddSingleton<ILogErrorHandlerProvider, DefaultLogErrorHandlerProvider>();
        }
    }
}