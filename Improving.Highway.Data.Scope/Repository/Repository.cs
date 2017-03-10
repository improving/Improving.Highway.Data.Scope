namespace Improving.Highway.Data.Scope.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DbContextScope;
    using global::Highway.Data;
    using global::Highway.Data.Interceptors.Events;
    using global::Highway.Data.Repositories;

    public interface IRepository<in T>: IDomainRepository<T>
        where T : class , IDomain
    {
        IDbContextScopeFactory Scopes { get; }
    }

    public class Repository<T> : IRepository<T>
        where T : class, IDomain
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        public IDomainContext<T> DomainContext => _context;

        public IUnitOfWork Context => _context;

        public Repository(IDbContextScopeFactory scopeFactory, IAmbientDbContextLocator ambientDbContextLocator)
        {
            Scopes            = scopeFactory;
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        public IDbContextScopeFactory Scopes { get; }

        private IDomainContext<T> _context
        {
            get
            {
                var context = _ambientDbContextLocator.Get<T>();

                if (context == null)
                    throw new Exception("All calls to the Repository must happen within a using statement with an IDbContextScope.  Use the IDbContextScopeFactory to create the IDbContextScope.");

                return context;
            }
        }

        public IEnumerable<T1> Find<T1>(IQuery<T1> query)
        {
            OnBeforeQuery(new BeforeQuery(query));
            var result = query.Execute(_context);
            OnAfterQuery(new AfterQuery(result));
            return result;
        }

        public IEnumerable<TProjection> Find<TSelection, TProjection>(IQuery<TSelection, TProjection> query)
        {
            OnBeforeQuery(new BeforeQuery(query));
            var results = query.Execute(_context);
            OnAfterQuery(new AfterQuery(results));
            return results;
        }

        public T1 Find<T1>(IScalar<T1> query)
        {
            OnBeforeScalar(new BeforeScalar(query));
            var result = query.Execute(_context);
            OnAfterScalar(new AfterScalar(query));
            return result;
        }

        public void Execute(ICommand command)
        {
            OnBeforeCommand(new BeforeCommand(command));
            command.Execute(_context);
            OnAfterCommand(new AfterCommand(command));
        }

        public virtual Task ExecuteAsync(ICommand command)
        {
            var task = new Task(() => command.Execute(_context));
            task.Start();
            return task;
        }

        public virtual Task<T1> FindAsync<T1>(IScalar<T1> query)
        {
            var task = new Task<T1>(() => query.Execute(_context));
            task.Start();
            return task;
        }

        public virtual Task<IEnumerable<T1>> FindAsync<T1>(IQuery<T1> query)
        {
            var task = new Task<IEnumerable<T1>>(() => query.Execute(_context));
            task.Start();
            return task;
        }

        public virtual Task<IEnumerable<IProjection>> FindAsync<TSelection, IProjection>(IQuery<TSelection, IProjection> query)
            where TSelection : class
        {
            var task = new Task<IEnumerable<IProjection>>(() => query.Execute(_context));
            task.Start();
            return task;
        }

        public event EventHandler<BeforeQuery> BeforeQuery;

        protected virtual void OnBeforeQuery(BeforeQuery e)
        {
            BeforeQuery?.Invoke(this, e);
        }

        public event EventHandler<BeforeScalar> BeforeScalar;

        protected virtual void OnBeforeScalar(BeforeScalar e)
        {
            BeforeScalar?.Invoke(this, e);
        }

        public event EventHandler<BeforeCommand> BeforeCommand;

        protected virtual void OnBeforeCommand(BeforeCommand e)
        {
            BeforeCommand?.Invoke(this, e);
        }

        public event EventHandler<AfterQuery> AfterQuery;

        protected virtual void OnAfterQuery(AfterQuery e)
        {
            AfterQuery?.Invoke(this, e);
        }

        public event EventHandler<AfterScalar> AfterScalar;

        protected virtual void OnAfterScalar(AfterScalar e)
        {
            AfterScalar?.Invoke(this, e);
        }

        public event EventHandler<AfterCommand> AfterCommand;

        protected virtual void OnAfterCommand(AfterCommand e)
        {
            AfterCommand?.Invoke(this, e);
        }
    }
}