namespace Improving.Highway.Data.Scope.Test
{
    using System;
    using System.Linq.Dynamic;
    using Context;
    using Context.Foos;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Scope;

    [TestClass]
    public class RepositoryTest : TestScenario
    {
        [TestMethod, ExpectedException(typeof(Exception))]
        public void ThrowsExceptionWithNoScope()
        {
            var repository = Container.Resolve<IRepository<ITestDomain>>();
            repository.Find(new GetFoos());
        }

        [TestMethod, Ignore]
        public void ReturnsEntitiesWithAScope()
        {
            var repository = Container.Resolve<IRepository<ITestDomain>>();
            using (repository.Scopes.CreateReadOnly())
            {
                var result = repository.Find(new GetFoos());
                Assert.IsTrue(result.Any());
            }
        }
    }
}
