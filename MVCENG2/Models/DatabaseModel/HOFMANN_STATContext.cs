using System;
using System.Collections.Generic;
using HoffmanWebstatistic.Models.Hoffman;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HoffmanWebstatistic
{
    public partial class HOFMANN_STATContext : DbContext
    {
        public HOFMANN_STATContext()
        {
        }

        public HOFMANN_STATContext(DbContextOptions<HOFMANN_STATContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DtcContent> DtcContents { get; set; } = null!;
        public virtual DbSet<DtcsPath> DtcsPaths { get; set; } = null!;
        public virtual DbSet<JsonsPath> JsonsPaths { get; set; } = null!;
        public virtual DbSet<Mes2supPath> Mes2supPaths { get; set; } = null!;
        public virtual DbSet<Mes2supTelegram> Mes2supTelegrams { get; set; } = null!;
        public virtual DbSet<Mes2supTelegramsStand> Mes2supTelegramsStands { get; set; } = null!;
        public virtual DbSet<OkNokVal> OkNokVals { get; set; } = null!;
        public virtual DbSet<Operator> Operators { get; set; } = null!;
        public virtual DbSet<OperatorsPath> OperatorsPaths { get; set; } = null!;
        public virtual DbSet<OperatorsStand> OperatorsStands { get; set; } = null!;
        public virtual DbSet<OsVersions> OsVersions { get; set; } = null!;
        public virtual DbSet<Picture> Pictures { get; set; } = null!;
        public virtual DbSet<PicturesPath> PicturesPaths { get; set; } = null!;
        public virtual DbSet<ResultsJsonHeader> ResultsJsonHeaders { get; set; } = null!;
        public virtual DbSet<ResultsJsonTest> ResultsJsonTests { get; set; } = null!;
        public virtual DbSet<ResultsJsonValue> ResultsJsonValues { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<SendingStatusLog> SendingStatusLogs { get; set; } = null!;
        public virtual DbSet<Stand> Stands { get; set; } = null!;
        public virtual DbSet<Sup2mesPath> Sup2mesPaths { get; set; } = null!;
        public virtual DbSet<Sup2mesTelegram> Sup2mesTelegrams { get; set; } = null!;
        public virtual DbSet<Translate> Translates { get; set; } = null!;
        public virtual DbSet<TranslatesPath> TranslatesPaths { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<XsdSchema> XsdSchemas { get; set; } = null!;
        public virtual DbSet<XsdSchemasPurpose> XsdSchemasPurposes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SV-HOFMANN;Initial Catalog=HOFMANN_STAT;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Cyrillic_General_100_CS_AS_KS");

            modelBuilder.Entity<DtcContent>(entity =>
            {
                entity.ToTable("dtc_content");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Fdata)
                    .HasColumnType("xml")
                    .HasColumnName("FData");

                entity.Property(e => e.Fname)
                    .HasMaxLength(255)
                    .HasColumnName("FName");
            });

            modelBuilder.Entity<DtcsPath>(entity =>
            {
                entity.ToTable("dtcs_paths");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CLogin)
                    .HasMaxLength(50)
                    .HasColumnName("CLogin");

                entity.Property(e => e.CPassword)
                    .HasMaxLength(50)
                    .HasColumnName("CPassword");

                entity.Property(e => e.CPath)
                    .HasMaxLength(255)
                    .HasColumnName("CPath");

                entity.Property(e => e.StandId).HasColumnName("StandID");

                entity.HasOne(d => d.Stand)
                    .WithMany(p => p.DtcsPaths)
                    .HasForeignKey(d => d.StandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dtcs_paths_stands");
            });

            modelBuilder.Entity<JsonsPath>(entity =>
            {
                entity.ToTable("jsons_paths");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CLogin)
                    .HasMaxLength(50)
                    .HasColumnName("CLogin");

                entity.Property(e => e.CPassword)
                    .HasMaxLength(50)
                    .HasColumnName("CPassword");

                entity.Property(e => e.CPath)
                    .HasMaxLength(255)
                    .HasColumnName("CPath");

                entity.Property(e => e.StandId).HasColumnName("StandID");

                entity.HasOne(d => d.Stand)
                    .WithMany(p => p.JsonsPaths)
                    .HasForeignKey(d => d.StandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_jsons_paths_stands");
            });

            modelBuilder.Entity<Mes2supPath>(entity =>
            {
                entity.ToTable("mes2sup_paths");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CLogin)
                    .HasMaxLength(50)
                    .HasColumnName("CLogin");

                entity.Property(e => e.CPassword)
                    .HasMaxLength(50)
                    .HasColumnName("CPassword");

                entity.Property(e => e.CPath)
                    .HasMaxLength(255)
                    .HasColumnName("CPath");

                entity.Property(e => e.StandId).HasColumnName("StandID");

                entity.HasOne(d => d.Stand)
                    .WithMany(p => p.Mes2supPaths)
                    .HasForeignKey(d => d.StandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_mes2sup_paths_stands");
            });

            modelBuilder.Entity<Mes2supTelegram>(entity =>
            {
                entity.ToTable("mes2sup_telegrams");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Ordernum).HasMaxLength(20);

                entity.Property(e => e.TgContent)
                    .HasColumnType("xml")
                    .HasColumnName("TGContent");

                entity.Property(e => e.TgFilename)
                    .HasMaxLength(255)
                    .HasColumnName("TGFilename");

                entity.Property(e => e.Vin)
                    .HasMaxLength(17)
                    .HasColumnName("VIN");
            });

            modelBuilder.Entity<Mes2supTelegramsStand>(entity =>
            {
                entity.ToTable("mes2sup_telegrams_stands");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StandId).HasColumnName("StandID");

                entity.Property(e => e.TgId).HasColumnName("TGID");

                entity.Property(e => e.Transfered)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Stand)
                    .WithMany(p => p.Mes2supTelegramsStands)
                    .HasForeignKey(d => d.StandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_mes2sup_telegrams_stands_stands");

                entity.HasOne(d => d.Tg)
                    .WithMany(p => p.Mes2supTelegramsStands)
                    .HasForeignKey(d => d.TgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_mes2sup_telegrams_stands_telegrams");
            });

            modelBuilder.Entity<OkNokVal>(entity =>
            {
                entity.ToTable("ok_nok_val");

                entity.Property(e => e.Val).HasMaxLength(3);
            });

            modelBuilder.Entity<Operator>(entity =>
            {
                entity.ToTable("operators");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InactiveMark)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('FALSE')");

                entity.Property(e => e.ODescription)
                    .HasMaxLength(1024)
                    .HasColumnName("ODescription");

                entity.Property(e => e.OLogin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OLogin");

                entity.Property(e => e.OPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OPassword");
            });

            modelBuilder.Entity<OperatorsPath>(entity =>
            {
                entity.ToTable("operators_paths");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CLogin)
                    .HasMaxLength(50)
                    .HasColumnName("CLogin");

                entity.Property(e => e.CPassword)
                    .HasMaxLength(50)
                    .HasColumnName("CPassword");

                entity.Property(e => e.CPath)
                    .HasMaxLength(255)
                    .HasColumnName("CPath");

                entity.Property(e => e.StandId).HasColumnName("StandID");

                entity.HasOne(d => d.Stand)
                    .WithMany(p => p.OperatorsPaths)
                    .HasForeignKey(d => d.StandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_operators_paths_standss");
            });

            modelBuilder.Entity<OperatorsStand>(entity =>
            {
                entity.ToTable("operators_stands");

                entity.HasOne(d => d.Operator)
                    .WithMany(p => p.OperatorsStands)
                    .HasForeignKey(d => d.OperatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_operators_stands_operators");

                entity.HasOne(d => d.Stand)
                    .WithMany(p => p.OperatorsStands)
                    .HasForeignKey(d => d.StandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_operators_stands_stands");
            });

            modelBuilder.Entity<OsVersions>(entity =>
            {
                entity.ToTable("os_versions");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.OSVersion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OSVersion");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.ToTable("pictures");

                entity.HasIndex(e => e.PName, "UQ__pictures__42B46083AE6A6C73")
                    .IsUnique();

                entity.Property(e => e.PictureBytes).HasColumnType("image");

                entity.Property(e => e.PName)
                    .HasMaxLength(255)
                    .HasColumnName("PName");
            });

            modelBuilder.Entity<PicturesPath>(entity =>
            {
                entity.ToTable("pictures_paths");

                entity.Property(e => e.CLogin)
                    .HasMaxLength(50)
                    .HasColumnName("CLogin");

                entity.Property(e => e.CPassword)
                    .HasMaxLength(50)
                    .HasColumnName("CPassword");

                entity.Property(e => e.CPath)
                    .HasMaxLength(255)
                    .HasColumnName("CPath");

                entity.HasOne(d => d.Stand)
                    .WithMany(p => p.PicturesPaths)
                    .HasForeignKey(d => d.StandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pictures_paths_stands");
            });

            modelBuilder.Entity<ResultsJsonHeader>(entity =>
            {
                entity.ToTable("results_json_headers");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.JsonFilename).HasMaxLength(255);

                entity.Property(e => e.Ordernum).HasMaxLength(20);

                entity.Property(e => e.VIN)
                    .HasMaxLength(17)
                    .HasColumnName("VIN");

                entity.HasOne(d => d.Operator)
                    .WithMany(p => p.ResultsJsonHeaders)
                    .HasForeignKey(d => d.OperatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_results_json_operators");

                entity.HasOne(d => d.Stand)
                    .WithMany(p => p.ResultsJsonHeaders)
                    .HasForeignKey(d => d.StandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_results_json_headers_stands");
            });

            modelBuilder.Entity<ResultsJsonTest>(entity =>
            {
                entity.ToTable("results_json_tests");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.TName)
                    .HasMaxLength(255)
                    .HasColumnName("TName");

                entity.Property(e => e.TSpecname)
                    .HasMaxLength(50)
                    .HasColumnName("TSpecname");

                entity.HasOne(d => d.Header)
                    .WithMany(p => p.ResultsJsonTests)
                    .HasForeignKey(d => d.HeaderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_results_json_tests_results_json_headers");

                entity.HasOne(d => d.Res)
                    .WithMany(p => p.ResultsJsonTests)
                    .HasForeignKey(d => d.ResId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_results_json_tests_ok_nok_val");
            });

            modelBuilder.Entity<ResultsJsonValue>(entity =>
            {
                entity.ToTable("results_json_values");

                entity.Property(e => e.VName)
                    .HasMaxLength(100)
                    .HasColumnName("VName");

                entity.Property(e => e.VValue)
                    .HasMaxLength(50)
                    .HasColumnName("VValue");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.ResultsJsonValues)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_results_json_values_results_json_tests");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.RDescription)
                    .HasMaxLength(255)
                    .HasColumnName("RDescription");

                entity.Property(e => e.RName)
                    .HasMaxLength(50)
                    .HasColumnName("RName")
                    .HasDefaultValueSql("('user')");
            });

            modelBuilder.Entity<SendingStatusLog>(entity =>
            {
                entity.ToTable("sending_status_log");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(255);

                entity.Property(e => e.SourceFilePath).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(5);

                entity.Property(e => e.TargetFilePath).HasMaxLength(255);

                entity.HasOne(d => d.Stand)
                    .WithMany(p => p.SendingStatusLogs)
                    .HasForeignKey(d => d.StandId)
                    .HasConstraintName("FK__Files__StandName__1DB06A4F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SendingStatusLogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Files__UserId__1CBC4616");
            });

            modelBuilder.Entity<Stand>(entity =>
            {
                entity.ToTable("stands");

                entity.Property(e => e.DnsName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InactiveMark)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('FALSE')");

                entity.Property(e => e.IpAdress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OSVersionNavigationID).HasColumnName("OSVersionNavigationID");

                entity.Property(e => e.Placement).HasMaxLength(50);

                entity.Property(e => e.Project)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('HOFMANN')");

                entity.Property(e => e.StandName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StandNameDescription).HasMaxLength(255);

                entity.Property(e => e.StandType)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('UNKNOWN')");

                entity.Property(e => e.WorkplaceMes)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.OsVersionNavigation)
                    .WithMany(p => p.Stands)
                    .HasForeignKey(d => d.OSVersionNavigationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stands_os_versions");
            });

            modelBuilder.Entity<Sup2mesPath>(entity =>
            {
                entity.ToTable("sup2mes_paths");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CLogin)
                    .HasMaxLength(50)
                    .HasColumnName("CLogin");

                entity.Property(e => e.CPassword)
                    .HasMaxLength(50)
                    .HasColumnName("CPassword");

                entity.Property(e => e.CPath)
                    .HasMaxLength(255)
                    .HasColumnName("CPath");

                entity.Property(e => e.StandId).HasColumnName("StandID");

                entity.HasOne(d => d.Stand)
                    .WithMany(p => p.Sup2mesPaths)
                    .HasForeignKey(d => d.StandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sup2mes_paths_stands");
            });

            modelBuilder.Entity<Sup2mesTelegram>(entity =>
            {
                entity.ToTable("sup2mes_telegrams");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Ordernum).HasMaxLength(20);

                entity.Property(e => e.StandId).HasColumnName("StandID");

                entity.Property(e => e.TgContent)
                    .HasColumnType("xml")
                    .HasColumnName("TGContent");

                entity.Property(e => e.TgFilename)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TGFilename");

                entity.Property(e => e.Vin).HasMaxLength(17);

                entity.HasOne(d => d.Stand)
                    .WithMany(p => p.Sup2mesTelegrams)
                    .HasForeignKey(d => d.StandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sup2mes_telegrams_stands");
            });

            modelBuilder.Entity<Translate>(entity =>
            {
                entity.HasKey(e => e.EngVariant)
                    .HasName("PK_TRANSLATES");

                entity.ToTable("translates");

                entity.Property(e => e.EngVariant).HasMaxLength(255);

                entity.Property(e => e.RusVariant).HasMaxLength(255);
            });

            modelBuilder.Entity<TranslatesPath>(entity =>
            {
                entity.ToTable("translates_paths");

                entity.Property(e => e.CLogin)
                    .HasMaxLength(50)
                    .HasColumnName("CLogin");

                entity.Property(e => e.CPassword)
                    .HasMaxLength(50)
                    .HasColumnName("CPassword");

                entity.Property(e => e.CPath)
                    .HasMaxLength(255)
                    .HasColumnName("CPath");

                entity.HasOne(d => d.Stand)
                    .WithMany(p => p.TranslatesPaths)
                    .HasForeignKey(d => d.StandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_translates_paths_stands");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InactiveMark)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('FALSE')");

                entity.Property(e => e.ULogin)
                    .HasMaxLength(50)
                    .HasColumnName("ULogin");

                entity.Property(e => e.UPassword)
                    .HasMaxLength(50)
                    .HasColumnName("UPassword");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_users_roles");
            });

            modelBuilder.Entity<XsdSchema>(entity =>
            {
                entity.ToTable("xsd_schemas");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.XsdDescription).HasMaxLength(255);

                entity.Property(e => e.XsdSchemaFile).HasColumnName("XsdSchemaFile");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.XsdSchemas)
                    .HasForeignKey(d => d.PurposeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_xsd_schemas_xsd_schemas_purpose");
            });

            modelBuilder.Entity<XsdSchemasPurpose>(entity =>
            {
                entity.ToTable("xsd_schemas_purpose");

                entity.Property(e => e.XsdPurpose)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
