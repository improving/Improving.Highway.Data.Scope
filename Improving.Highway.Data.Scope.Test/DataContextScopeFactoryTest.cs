namespace Improving.Highway.Data.Scope.Test
{
    using Improving.Highway.Data.Scope.DbContextScope;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DbContextScopeFactoryTests : TestScenario
    {
        [TestMethod]
        public void CanResolveDbContextScope()
        {
           var factory = Container.Resolve<IDbContextScopeFactory>();

            //Act
            var dbContextScope = factory.Create();

            //Assert
            Assert.IsNotNull(dbContextScope);
        }
    }
}
