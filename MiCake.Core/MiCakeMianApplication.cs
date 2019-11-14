﻿using System;
using System.Collections.Generic;
using System.Text;
using MiCake.Core.Abstractions;
using MiCake.Core.Abstractions.Builder;
using MiCake.Core.Abstractions.Modularity;
using MiCake.Core.Util;
using Microsoft.Extensions.DependencyInjection;

namespace MiCake.Core
{
    public class DefaultMiCakeApplicationProvider : MiCakeApplication, IMiCakeApplicationProvider
    {
        public DefaultMiCakeApplicationProvider(Type startUp, IServiceCollection services) : base(startUp, services)
        {
            services.AddSingleton<IMiCakeApplicationProvider>(this);
        }

        public IMiCakeApplication GetApplication()
        {
            return this;
        }

        public void Initialize(IServiceProvider serviceProvider)
        {
            McCheckValue.NotNull(serviceProvider, nameof(serviceProvider));

            SetServiceProvider(serviceProvider);

            Init();
        }
    }
}
