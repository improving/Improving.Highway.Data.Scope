namespace Improving.Highway.Data.Scope.Test.Context
{
    using System.Data.Entity;

    public class TestDomainContextInitializer : DropCreateDatabaseIfModelChanges<TestDomainContext>
    {
        protected override void Seed(TestDomainContext context)
        {
            context.Add(new Foo {Name = "foo1"});
            context.Add(new Foo {Name = "foo2"});
            context.Commit();
        }
    }
}