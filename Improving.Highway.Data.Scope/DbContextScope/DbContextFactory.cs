namespace Improving.Highway.Data.Scope
{
    using System;
    using System.Data.Entity;
    using Castle.Core.Logging;
    using Castle.MicroKernel;
    using global::Highway.Data;

    public class DbContextFactory : IDbContextFactory
    {
        private readonly IKernel _kernel;
        private ILogger _logger;
        private Action<string> _log;

        public DbContextFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public ILogger Logger
        {
            get { return _logger; }
            set
            {
                _logger = value;
                _log = (_logger != null)
                     ? _logger.Debug
                     : null as Action<string>;
            }
        }

        public IDomainContext<TDomain> CreateDbContext<TDomain>() where TDomain : class, IDomain
        {
            var dbContext = _kernel.Resolve<IDomainContext<TDomain>>();
            var dbContextInstance = dbContext as DbContext;
            if (dbContextInstance != null)
                ConfigureDbContext(dbContextInstance);
            return dbContext;
        }

        public void Release(object instance)
        {
            _kernel.ReleaseComponent(instance);
        }

        protected virtual void ConfigureDbContext(DbContext dbContext)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            if (_log != null && Logger.IsDebugEnabled)
                dbContext.Database.Log = _log;
        }
    }
}
