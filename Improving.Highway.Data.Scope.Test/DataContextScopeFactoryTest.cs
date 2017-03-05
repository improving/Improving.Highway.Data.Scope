namespace Improving.Highway.Data.Scope.Test
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Scope;

    [TestClass]
    public class DbContextScopeFactoryTests
    {
        [TestMethod]
        public void CanResolveDbContextScope()
        {
            //Arrange
            var container = new WindsorContainer()
                .Install(new RepositoryInstaller(Classes.FromThisAssembly()));

            var factory = container.Resolve<IDbContextScopeFactory>();

            //Act
            var dbContextScope = factory.Create();

            //Assert
            Assert.IsNotNull(dbContextScope);
        }
    }
}
