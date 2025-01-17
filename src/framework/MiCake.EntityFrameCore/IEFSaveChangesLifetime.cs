﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MiCake.EntityFrameworkCore
{
    /// <summary>
    /// EFCore life cycle hook when saveChanges method is executed
    /// </summary>
    public interface IEFSaveChangesLifetime
    {
        /// <summary>
        /// Before EFCore SaveChanges().
        /// </summary>
        /// <param name="entityEntries">current change tracking entites</param>
        void BeforeSaveChanges(IEnumerable<EntityEntry> entityEntries);

        /// <summary>
        /// Before EFCore SaveChangesAsync().
        /// </summary>
        /// <param name="entityEntries">current change tracking entites</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task BeforeSaveChangesAsync(IEnumerable<EntityEntry> entityEntries, CancellationToken cancellationToken = default);

        /// <summary>
        /// After EFCore SaveChanges().
        /// </summary>
        /// <param name="entityEntries">current change tracking entites</param>
        void AfterSaveChanges(IEnumerable<EntityEntry> entityEntries);

        /// <summary>
        /// After EFCore SaveChanges().
        /// </summary>
        /// <param name="entityEntries">current change tracking entites</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AfterSaveChangesAsync(IEnumerable<EntityEntry> entityEntries, CancellationToken cancellationToken = default);
    }
}
