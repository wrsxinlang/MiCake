﻿using MiCake.Core.DependencyInjection;
using System;

namespace MiCake.EntityFrameworkCore
{
    public class MiCakeEFCoreOptions : IObjectAccessor<MiCakeEFCoreOptions>
    {
        /// <summary>
        /// <para>
        /// Whether to register default repository.
        /// </para>
        /// <para>
        /// If is true,MiCake will register default repository to ISeviceCollection.Like: IRepository<MyDemoAggregate,Guid>
        /// </para>
        /// <para>
        /// Default value is true.
        /// </para>
        /// </summary>
        public bool RegisterDefaultRepository { get; set; } = true;

        /// <summary>
        /// <para>
        /// Whether to register default free repository.
        /// </para>
        /// <para>
        /// If is true,MiCake will register default free repository to ISeviceCollection.Like: IFreeRepository<MyDemoAggregate,Guid>
        /// </para>
        /// <para>
        /// Default value is false.Beacuse MiCake recommends  you use default repository.
        /// </para>
        /// </summary>
        public bool RegisterFreeRepository { get; set; } = false;

        /// <summary>
        /// Type of <see cref="MiCakeDbContext"/>.
        /// </summary>
        public Type DbContextType { get; private set; }

        MiCakeEFCoreOptions IObjectAccessor<MiCakeEFCoreOptions>.Value => this;

        public MiCakeEFCoreOptions(Type dbContextType)
        {
            DbContextType = dbContextType;
        }
    }
}
