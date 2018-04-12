using System.Data.Entity;
using Repository.Models;

namespace Repository.Services
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DbConnection")
        {
        }

        public DbSet<Image> Images { get; set; }
    }
}