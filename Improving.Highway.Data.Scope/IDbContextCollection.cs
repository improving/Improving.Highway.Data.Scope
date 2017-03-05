/*
 * Copyright (C) 2014 Mehdi El Gueddari
 * http://mehdi.me
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 */

namespace Improving.Highway.Data.Scope
{
    using System;
    using global::Highway.Data;

    /// <summary>
    /// Maintains a list of lazily-created DbContext instances.
    /// </summary>
    public interface IDbContextCollection : IDisposable
    {
        /// <summary>
        /// Get or create a DbContext instance of the specified type.
        /// </summary>
		IDomainContext<TDomain> Get<TDomain>() where TDomain : class, IDomain;
    }
}