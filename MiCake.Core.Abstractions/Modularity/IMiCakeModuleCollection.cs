﻿using System.Collections.Generic;
using System.Reflection;

namespace MiCake.Core.Abstractions.Modularity
{
    /// <summary>
    /// Specifies the contract for a collection of <see cref="MiCakeModuleDescriptor"/>.
    /// </summary>
    public interface IMiCakeModuleCollection:IList<MiCakeModuleDescriptor>
    {
        //Get the assemblies in all micake modules
        Assembly[] GetAllReferAssembly();
    }
}
