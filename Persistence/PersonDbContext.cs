
namespace ERP.Person.Repository
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> dbContextOptions):base(dbContextOptions) 
        {
        }

        public DbSet<ERP.Person.Models.Entities.Person> People { get; set; }  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
