using System.Data.Entity;

namespace Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DbConnection")
        {
        }

        public DbSet<Image> Images { get; set; }
    }
}