using System.Data.Entity;

namespace Shared.Contract
{
    public interface IDbFactory
    {
        DbContext Create();
    }
}