using Gusto_Test_MultiTenantApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gusto_Test_MultiTenantApp.DbContexts
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> option) : base(option)
        {
        }

        public DbSet<Tenant> Tenant { get; set; }
        public DbSet<TenantUser> TenantUser { get; set; }

    }
}
