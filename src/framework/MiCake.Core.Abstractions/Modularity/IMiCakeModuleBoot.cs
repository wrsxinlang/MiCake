﻿using System;

namespace MiCake.Core.Modularity
{
    /// <summary>
    /// Micake module boot.use to initialization module and shutdown module.
    /// </summary>
    public interface IMiCakeModuleBoot
    {
        void ConfigServices(ModuleConfigServiceContext context);

        void Initialization(ModuleBearingContext context);

        void ShutDown(ModuleBearingContext context);

        /// <summary>
        /// Add other configservice actions. 
        /// This part of the operation will be called at the end of ConfigServices().
        /// </summary>
        /// <param name="configServiceAction"></param>
        void AddConfigService(Action<ModuleConfigServiceContext> configServiceAction);

        /// <summary>
        /// Add other initalzation actions. 
        /// This part of the operation will be called at the end of Initialization().
        /// </summary>
        /// <param name="initalzationAction"></param>
        void AddInitalzation(Action<ModuleBearingContext> initalzationAction);
    }
}
