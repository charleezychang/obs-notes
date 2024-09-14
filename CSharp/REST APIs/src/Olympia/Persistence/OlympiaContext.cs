using Microsoft.EntityFrameworkCore;

namespace Olympia.Persistence
{
    public class OlympiaContext : DbContext
    {
        public OlympiaContext(DbContextOptions<OlympiaContext> options) : base(options)
        {

            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseIfModelChanges<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseAlways<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new SchoolDBInitializer());
        }

        public DbSet<Product> Products { get; set; }
    }
}
