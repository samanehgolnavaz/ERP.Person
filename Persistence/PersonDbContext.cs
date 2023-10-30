
namespace ERP.Person.Repository
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> dbContextOptions):base(dbContextOptions) 
        {
        }

        public DbSet<Models.Entities.Person> People { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs => base.Set<ExceptionLog>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
