﻿using MiCake.Core.Abstractions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiCake.Core.Tests.DependencyInjection.Fakes
{
    public class AutoInjectSingletonClass : ISingletonService
    {
        public string BackString()
        {
            return "this is SingletonClass";
        }
    }
}