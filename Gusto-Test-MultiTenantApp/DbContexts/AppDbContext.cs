using Gusto_Test_MultiTenantApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gusto_Test_MultiTenantApp.DbContexts
{
    public class AppDbContext:DbContext
    {
        private readonly Tenant tenant;
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, Tenant tenant, IConfiguration configuration) : base(options)
        {
            this.tenant = tenant;
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = _configuration.GetConnectionString("ApplicationConnection");

            optionsBuilder.UseSqlServer(conn);
        }

        public DbSet<User> User { get; set; }
    }
}
