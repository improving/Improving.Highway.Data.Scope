namespace Improving.Highway.Data.Scope
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using global::Highway.Data;
    using Component = Castle.MicroKernel.Registration.Component;

    public class RepositoryInstaller : IWindsorInstaller
    {
        private readonly FromAssemblyDescriptor[] _fromAssemblies;

        public RepositoryInstaller(params FromAssemblyDescriptor[] fromAssemblies)
        {
            _fromAssemblies = fromAssemblies;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDbContextFactory>().ImplementedBy<DbContextFactory>(),
                Component.For<IDbContextScopeFactory>().ImplementedBy<DbContextScopeFactory>(),
                Component.For<IAmbientDbContextLocator>().ImplementedBy<AmbientDbContextLocator>(),
                Component.For(typeof (IRepository<>)).ImplementedBy(typeof (Repository<>))
            );

            foreach (var assemebly in _fromAssemblies)
            {
                container.Register(
                    assemebly.BasedOn<IMappingConfiguration>().WithServiceFromInterface()
                        .Configure(config => config.LifestyleSingleton()
                        .OnlyNewServices()),
                    assemebly.BasedOn<IDomain>().WithServiceFromInterface()
                        .Configure(config => config.LifestyleSingleton()
                        .OnlyNewServices()),

                    assemebly.BasedOn(typeof(IDomainContext<>)).WithServiceFromInterface()
                        .Configure(config => config.LifestyleTransient()
                              .OnlyNewServices()));
            }
        }
    }
}