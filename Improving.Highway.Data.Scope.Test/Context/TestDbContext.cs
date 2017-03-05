namespace Improving.Highway.Data.Scope.Test.Context
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Common.Logging;
    using global::Highway.Data;
    using global::Highway.Data.EventManagement.Interfaces;
    using global::Highway.Data.Interceptors.Events;

    public interface ITestDomain : IDomain
    {
    }

    public class TestDomain : ITestDomain
    {
        public string ConnectionString { get; } =
            "Data Source=(local);Initial Catalog=Scope_Repository_Test;Integrated Security=True;MultipleActiveResultSets=True";
        public IMappingConfiguration Mappings { get; }
        public IContextConfiguration Context  { get; }
        public List<IInterceptor>    Events   { get; }
    }

    public class TestDomainContext : DataContext, IDomainContext<ITestDomain>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new TestDomainContextInitializer());
            modelBuilder.Entity<Foo>();
        }


        public TestDomainContext(IMappingConfiguration mapping) : base(nameof(TestDomainContext), mapping)
        {
        }

        public TestDomainContext(string connectionString, IMappingConfiguration mapping) : base(connectionString, mapping)
        {
        }

        public TestDomainContext(string connectionString, IMappingConfiguration mapping, ILog log) : base(connectionString, mapping, log)
        {
        }

        public TestDomainContext(string connectionString, IMappingConfiguration mapping, IContextConfiguration contextConfiguration) : base(connectionString, mapping, contextConfiguration)
        {
        }

        public TestDomainContext(string connectionString, IMappingConfiguration mapping, IContextConfiguration contextConfiguration, ILog log) : base(connectionString, mapping, contextConfiguration, log)
        {
        }

        public TestDomainContext(string databaseFirstConnectionString) : base(databaseFirstConnectionString)
        {
        }

        public TestDomainContext(string databaseFirstConnectionString, ILog log) : base(databaseFirstConnectionString, log)
        {
        }

        public event EventHandler<BeforeSave> BeforeSave;
        public event EventHandler<AfterSave> AfterSave;
    }
}