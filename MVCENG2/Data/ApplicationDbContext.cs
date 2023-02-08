using Microsoft.EntityFrameworkCore;
using MVCENG2.Models;

namespace MVCENG2.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }
        

        public DbSet<Stand> Stand { get; set; }

        public DbSet<Statistic> Statistic { get; set; }

        public DbSet<TestReport> TestReport { get; set; }

    }
}
