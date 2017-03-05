namespace Improving.Highway.Data.Scope.Test.Context
{
    using System;
    using global::Highway.Data;
    using Scope;

    public class TestDbContextFactory : IDbContextFactory
    {
        public IDomainContext<TDomain> CreateDbContext<TDomain>() where TDomain : class, IDomain
        {
            var domain = new TestDomain();
            return (IDomainContext<TDomain>) new TestDomainContext(domain.ConnectionString);
        }

        public void Release(object instance)
        {
            throw new NotImplementedException();
        }
    }
}