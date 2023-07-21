using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PingerAPI.Models;
using PingerAPI.Models.General;
using PingerAPI.Models.Hoffman;

using System;

namespace PingerWebAPI.Repository
{
    public class ApplicationDbContext : DbContext
    {
        //private readonly StreamWriter _logStream = new StreamWriter("Logs\\EntityFrameworkLogs.txt", append: true);
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
            //_logStream.Dispose();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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

    }
}
