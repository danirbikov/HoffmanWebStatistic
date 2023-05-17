using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCENG2.Models.General;
using MVCENG2.Models.Hoffman;
using MVCENG2.Models.Siemens;

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

        public DbSet<Stand> stands { get; set; }
        public DbSet<Operator> operators { get; set; }

        public DbSet<ResultsJsonHeader> results_json_headers { get; set; }
        public DbSet<ResultsJsonTest> results_json_tests { get; set; }

        public DbSet<ResultsJsonValue> results_json_values { get; set; }
        public DbSet<Statistic> Statistic { get; set; }

        public DbSet<TestReport> TestReport { get; set; }
        public DbSet<ResultDataJSON_OLDTEST> TestJson { get; set; }

    }
}
