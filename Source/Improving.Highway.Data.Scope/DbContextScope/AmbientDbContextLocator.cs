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

    public class AmbientDbContextLocator : IAmbientDbContextLocator
    {
        public IDomainContext<TDomain> Get<TDomain>() where TDomain : class, IDomain
        {
            var ambientDbContextScope = DbContextScope.GetAmbientScope();
            return ambientDbContextScope?.DbContexts.Get<TDomain>();
        }
    }
}