using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using HoffmanWebstatistic.Models.General;
using HoffmanWebstatistic.Models.Hoffman;
using HoffmanWebstatistic.Models.Siemens;

namespace HoffmanWebstatistic.Data
{
    public class ApplicationDbContext : DbContext
    {
        //private readonly StreamWriter logStream = new StreamWriter("C:\\WebStatistic\\Logs\\EFLogs.txt", true);
        public ApplicationDbContext()
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.EnableSensitiveDataLogging();
        //optionsBuilder.LogTo(_logStream.WriteLine);
        // }

        public override void Dispose()
        {
            base.Dispose();
            //logStream.Dispose();
        }
        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            //await logStream.DisposeAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.LogTo(logStream.WriteLine);

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();

        }

        public DbSet<Stand> stands { get; set; }
        public DbSet<Operator> operators { get; set; }
        public DbSet<ResultsJsonHeader> results_json_headers { get; set; }
        public DbSet<ResultsJsonTest> results_json_tests { get; set; }
        public DbSet<ResultsJsonValue> results_json_values { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Picture> pictures { get; set; }
        public DbSet<PicturesPath> pictures_path { get; set; }
        public DbSet<OkNokVal> ok_nok_val { get; set; }
        public DbSet<Translate> translates { get; set; }
        public DbSet<TranslatesPath> translates_path { get; set; }

    }
}
