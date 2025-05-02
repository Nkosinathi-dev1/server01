using Microsoft.EntityFrameworkCore;
using VisitorService.Models;

namespace VisitorService.Data 
{
    public class VisitorDbContext : DbContext
    {
        public VisitorDbContext(DbContextOptions<VisitorDbContext> options) : base(options) 
        {
            // I am leaving this part empty for Now...
        }

        public DbSet<Visitor> Visitors => Set<Visitor>();
        public DbSet<VisitLog> VisitLogs => Set<VisitLog>();
    }
}
