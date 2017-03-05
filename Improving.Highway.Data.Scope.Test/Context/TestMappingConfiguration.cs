namespace Improving.Highway.Data.Scope.Test.Context
{
    using System.Data.Entity;
    using global::Highway.Data;

    public class TestMappingConfiguration: IMappingConfiguration
    {
        public void ConfigureModelBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);
        }
    }
}
