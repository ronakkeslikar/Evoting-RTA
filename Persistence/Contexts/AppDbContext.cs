using Microsoft.EntityFrameworkCore;

using evoting.Persistence.Contexts.Sp_SQL_Objects;

namespace evoting.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {            
            
        }
    }
}