namespace Improving.Highway.Data.Scope.Test
{
    using System;
    using System.Linq;
    using System.Text;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Handlers;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Castle.Windsor.Diagnostics;
    using Context;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Scope;

    [TestClass]
    public class RepositoryInstallerTests
    {
        //[TestMethod]
        //public void ContainerHasAllDependenciesRegistered()
        //{
        //    ////Arrange
        //    //var container = new WindsorContainer()
        //    //    .Install(new RepositoryInstaller(Classes.FromThisAssembly()));

        //    //var host = (IDiagnosticsHost)container.Kernel.GetSubSystem(SubSystemConstants.DiagnosticsKey);
        //    //var diagnostics = host.GetDiagnostic<IPotentiallyMisconfiguredComponentsDiagnostic>();
        //    //var handlers = diagnostics.Inspect();

        //    ////Assert
        //    //if (handlers.Any())
        //    //{
        //    //    var message = new StringBuilder();
        //    //    var inspector = new DependencyInspector(message);

        //    //    foreach (var handler in handlers)
        //    //    {
        //    //        var expose = handler as IExposeDependencyInfo;
        //    //        if (expose != null)
        //    //          expose.ObtainDependencyDetails(inspector);
        //    //    }

        //    //    Console.WriteLine(message);
        //    //    Assert.AreEqual(0, message.Length);
        //    //}
        //}

        [TestMethod]
        public void CanResolveARepositoryOfT()
        {
            //Arrange
            var container = new WindsorContainer()
                .Install(new RepositoryInstaller(Classes.FromThisAssembly()));

            //Act
            var repository = container.Resolve<IRepository<ITestDomain>>();

            //Assert
            Assert.IsNotNull(repository);
        }
    }
}
