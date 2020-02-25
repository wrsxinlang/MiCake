﻿using System;

namespace MiCake.Core.Modularity
{
    public class ModuleBearingContext
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IMiCakeModuleCollection MiCakeModules { get; private set; }
        public MiCakeApplicationOptions MiCakeApplicationOptions { get; set; }

        public ModuleBearingContext(
            IServiceProvider serviceProvider,
            IMiCakeModuleCollection miCakeModules,
            MiCakeApplicationOptions applicationOptions
            )
        {
            ServiceProvider = serviceProvider;
            MiCakeModules = miCakeModules;
            MiCakeApplicationOptions = applicationOptions;
        }
    }
}
