using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCENG2.Models;

namespace MVCENG2.Data
{
    public class ApplicationDbContext : DbContext
    {
        //private readonly StreamWriter _logStream = new StreamWriter("Logs\\EntityFrameworkLogs.txt", append: true);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            //optionsBuilder.LogTo(_logStream.WriteLine);
        }
            
            

        public override void Dispose()
        {
            base.Dispose();
            //_logStream.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            //await _logStream.DisposeAsync();
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            Database.EnsureCreated();
            
        }

        public DbSet<Stand> Stand { get; set; }

        public DbSet<Statistic> Statistic { get; set; }

        public DbSet<TestReport> TestReport { get; set; }
        public DbSet<TestJSON> TestJson { get; set; }

    }
}
