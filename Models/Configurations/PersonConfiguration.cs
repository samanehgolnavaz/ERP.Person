using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Person.Models.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Entities.Person>
    {
        public void Configure(EntityTypeBuilder<Entities.Person> builder)
        {
            builder.HasIndex(c => c.Email);
            builder.HasIndex(c => c.NationalId);
            builder.Property(c => c.Email).HasMaxLength(200);
        }
    }
}
