/*
 * Copyright (C) 2014 Mehdi El Gueddari
 * http://mehdi.me
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 */

namespace Improving.Highway.Data.Scope.DbContextScope
{
    using global::Highway.Data;

    /// <summary>
    /// Convenience methods to retrieve ambient DbContext instances.
    /// </summary>
    public interface IAmbientDbContextLocator
    {
        /// <summary>
        /// If called within the scope of a DbContextScope, gets or creates
        /// the ambient DbContext instance for the provided DbContext type.
        ///
        /// Otherwise returns null.
        /// </summary>
        IDomainContext<TDomain> Get<TDomain>() where TDomain : class, IDomain;
    }
}
