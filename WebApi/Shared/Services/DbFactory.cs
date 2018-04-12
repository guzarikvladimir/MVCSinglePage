using System.Data.Entity;
using Repository.Services;
using Shared.Contract;

namespace Shared.Services
{
    public class DbFactory : IDbFactory
    {
        public DbContext Create()
        {
            return new ApplicationContext();
        }
    }
}