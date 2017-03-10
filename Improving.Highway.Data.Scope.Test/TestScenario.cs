namespace Improving.Highway.Data.Scope.Test
{
    using System.Data.Entity;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Context;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using global::Improving.Highway.Data.Scope.Repository;

    public abstract class TestScenario
    {
        public IWindsorContainer Container { get; set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Database.SetInitializer(new TestDomainContextInitializer());

            Container = new WindsorContainer()
                .Install(new RepositoryInstaller(Classes.FromThisAssembly()));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Container.Dispose();
        }
    }
}