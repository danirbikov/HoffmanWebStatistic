using Microsoft.EntityFrameworkCore;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext()
        {
        }

        public override void Dispose()
        {
            base.Dispose();

        }
        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();

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
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Picture> pictures { get; set; }
        public DbSet<OkNokVal> ok_nok_val { get; set; }
        public DbSet<Translate> translates { get; set; }
        public DbSet<TranslatesPath> translates_paths { get; set; }
        public DbSet<SendingStatusLog> sending_status_log { get; set; }
        public DbSet<PicturesPath> pictures_paths { get; set; }
        public DbSet<JsonsPath> jsons_paths { get; set; }
        public DbSet<OperatorsPath> operators_paths { get; set; }
        public DbSet<DtcsPath> dtcs_paths { get; set; }
        public DbSet<DtcContent> dtc_content { get; set; }

        public DbSet<Mes2supPath> mes2sup_paths { get; set; }
        public DbSet<Mes2supTelegram> mes2sup_telegrams { get; set; }
        public DbSet<Mes2supTelegramsStand> mes2sup_telegrams_stands { get; set; }
        public DbSet<Sup2mesPath> sup2mes_paths { get; set; }
        public DbSet<Sup2mesTelegram> sup2mes_telegrams { get; set; }
        public DbSet<XsdSchema> xsd_schemas { get; set; }
        public DbSet<XsdSchemasPurpose> xsd_schemas_purpose { get; set; }
        public DbSet<MesPathsCredential> mes_paths_credentials { get; set; }
    }
    
}
