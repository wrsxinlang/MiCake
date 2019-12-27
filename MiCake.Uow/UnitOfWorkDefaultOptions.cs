﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MiCake.Uow
{
    /// <summary>
    /// this option is default unit of work setting
    /// if create unit of work without option,will use this config
    /// </summary>
    public class UnitOfWorkDefaultOptions
    {
        public IsolationLevel? IsolationLevel { get; set; }

        public TimeSpan? Timeout { get; set; }

        public UnitOfWorkLimit Limit { get; set; }

        public UnitOfWorkDefaultOptions()
        {
            Limit = UnitOfWorkLimit.Required;
        }
    }

    public static class UnitOfWorkDefaultOptionsExtensiong
    {
        /// <summary>
        /// convert UnitOfWorkDefaultOptions to UnitOfWorkOptions.
        /// </summary>
        public static UnitOfWorkOptions ConvertToUnitOfWorkOptions(this UnitOfWorkDefaultOptions defaultOptions)
        {
            return new UnitOfWorkOptions(defaultOptions.IsolationLevel, defaultOptions.Timeout, defaultOptions.Limit);
        }
    }
}
