
namespace ERP.Person.Models.Entities.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasIndex(c => c.Email);
            builder.HasIndex(c => c.NationalId);
            builder.Property(c => c.Email).HasMaxLength(200);
            builder.Property(c => c.NationalId).HasMaxLength(10);
            builder.HasIndex(c => c.NationalId).IsUnique();

        }
    }
}
