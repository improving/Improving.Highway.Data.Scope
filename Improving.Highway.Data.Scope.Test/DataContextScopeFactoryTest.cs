namespace Improving.Highway.Data.Scope.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Scope;

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
