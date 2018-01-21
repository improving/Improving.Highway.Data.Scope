namespace Improving.Highway.Data.Scope.Test.Context.Foos
{
    using global::Highway.Data;

    public class GetFoos : Query<Foo>
    {
        public GetFoos()
        {
            ContextQuery = c => c.AsQueryable<Foo>();
        }
    }
}