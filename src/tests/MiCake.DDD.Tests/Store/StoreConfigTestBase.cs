﻿using MiCake.DDD.Extensions.Store.Configure;

namespace MiCake.DDD.Tests.Store
{
    public abstract class StoreConfigTestBase
    {
        protected IStoreModel CreateModel() => new StoreModel();

        protected StoreModelBuilder CreateStoreModelBuilder() => new StoreModelBuilder(CreateModel());
    }
}
