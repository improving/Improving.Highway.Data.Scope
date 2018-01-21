namespace Improving.Highway.Data.Scope.Test.Context.Foos
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public class FooMap : EntityTypeConfiguration<Foo>
    {
        public FooMap()
        {
            ToTable(nameof(Foo));
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasMaxLength(30);
        }
    }
}