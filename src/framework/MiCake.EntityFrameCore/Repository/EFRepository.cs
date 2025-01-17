﻿using MiCake.DDD.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiCake.EntityFrameworkCore.Repository
{
    public class EFRepository<TDbContext, TAggregateRoot, TKey> :
        EFReadOnlyRepository<TDbContext, TAggregateRoot, TKey>,
        IRepository<TAggregateRoot, TKey>
        where TAggregateRoot : class, IAggregateRoot<TKey>
        where TDbContext : DbContext
    {
        public EFRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public virtual void Add(TAggregateRoot aggregateRoot)
            => DbContext.Add(aggregateRoot);

        public virtual TAggregateRoot AddAndReturn(TAggregateRoot aggregateRoot, bool autoExecute = true)
        {
            var entity = DbContext.Add(aggregateRoot);

            if (autoExecute)
            {
                /*
                 * Depending on the database provider being used, values may be generated client side by EF or in the database. 
                 * If the value is generated by the database, then EF may assign a temporary value when you add the entity to the context.
                 * This temporary value will then be replaced by the database generated value during SaveChanges().
                 * 
                 * https://docs.microsoft.com/en-us/ef/core/modeling/generated-properties?tabs=data-annotations
                 **/
                DbContext.SaveChanges();
            }

            return entity.Entity;
        }
        public virtual async Task<TAggregateRoot> AddAndReturnAsync(TAggregateRoot aggregateRoot, bool autoExecute = true, CancellationToken cancellationToken = default)
        {
            var entityInfo = await DbContext.AddAsync(aggregateRoot, cancellationToken);

            if (autoExecute)
            {
                /*
                 * Depending on the database provider being used, values may be generated client side by EF or in the database. 
                 * If the value is generated by the database, then EF may assign a temporary value when you add the entity to the context.
                 * This temporary value will then be replaced by the database generated value during SaveChanges().
                 * 
                 * https://docs.microsoft.com/en-us/ef/core/modeling/generated-properties?tabs=data-annotations
                 **/
                await DbContext.SaveChangesAsync(cancellationToken);
            }
            return entityInfo.Entity;
        }

        public virtual async Task AddAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
            => await DbContext.AddAsync(aggregateRoot, cancellationToken);

        public virtual void Delete(TAggregateRoot aggregateRoot)
            => DbContext.Remove(aggregateRoot);

        public virtual Task DeleteAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
        {
            DbContext.Remove(aggregateRoot);
            return Task.CompletedTask;
        }

        public virtual void Update(TAggregateRoot aggregateRoot)
            => DbContext.Update(aggregateRoot);

        public virtual Task UpdateAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
        {
            DbContext.Update(aggregateRoot);
            return Task.CompletedTask;
        }
    }
}
