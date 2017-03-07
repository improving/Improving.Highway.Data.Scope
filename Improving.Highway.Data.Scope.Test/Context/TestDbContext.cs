namespace Improving.Highway.Data.Scope.Test.Context
{
    using System.Collections.Generic;
    using Common.Logging;
    using global::Highway.Data;
    using global::Highway.Data.EventManagement.Interfaces;

    public interface ITestDomain : IDomain
    {
    }

    public class TestDomain : ITestDomain
    {
        public string ConnectionString { get; } =
            "Data Source=(local);Initial Catalog=Scope_Repository_Test;Integrated Security=True;MultipleActiveResultSets=True";
        public IMappingConfiguration Mappings { get; set; }
        public IContextConfiguration Context  { get; set; }
        public List<IInterceptor>    Events   { get; set; }
    }

    public class TestDomainContext : DomainContext<ITestDomain>, IDomainContext<IDomain>
    {
        public TestDomainContext(ITestDomain domain) : base(domain)
        {
        }

        public TestDomainContext(ITestDomain domain, ILog logger) : base(domain, logger)
        {
        }
    }
}