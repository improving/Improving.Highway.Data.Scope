namespace Improving.Highway.Data.Scope
{
    using System;
    using global::Highway.Data;
    using global::Highway.Data.Repositories;

    public interface IRepository<out T>: IRepository
        where T : class , IDomain
    {
        IDbContextScopeFactory Scopes { get; }
    }

    public class Repository<TDomain> : DomainRepository<TDomain>, IRepository<TDomain>
        where TDomain : class, IDomain
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        public Repository(IDomainContext<TDomain> context, TDomain domain,
            IDbContextScopeFactory scopeFactory, IAmbientDbContextLocator ambientDbContextLocator)
            :base(context, domain)
        {
            Scopes            = scopeFactory;
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        public IDbContextScopeFactory Scopes { get; }

        protected new IDataContext Context
        {
            get
            {
                var context = _ambientDbContextLocator.Get<TDomain>();

                if (context == null)
                    throw new Exception("All calls to the Repository must happen within a using statement with an IDbContextScope.  Use the IDbContextScopeFactory to create the IDbContextScope.");

                return context;
            }
        }
    }
}