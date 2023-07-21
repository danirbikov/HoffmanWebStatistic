using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PingerAPI.Models.General;

namespace PingerAPI.Models.Hoffman
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

        public virtual DbSet<Mes2supPath> Mes2supPaths { get; set; } = null!;
        public virtual DbSet<Mes2supTelegram> Mes2supTelegrams { get; set; } = null!;
        public virtual DbSet<Mes2supTelegramsStand> Mes2supTelegramsStands { get; set; } = null!;
        public virtual DbSet<OkNokVal> OkNokVals { get; set; } = null!;
        public virtual DbSet<Operator> Operators { get; set; } = null!;
        public virtual DbSet<OperatorsStand> OperatorsStands { get; set; } = null!;
        public virtual DbSet<OsVersions> OsVersions { get; set; } = null!;
        public virtual DbSet<Picture> Pictures { get; set; } = null!;
        public virtual DbSet<PicturesPath> PicturesPaths { get; set; } = null!;
        public virtual DbSet<ResultsJsonHeader> ResultsJsonHeaders { get; set; } = null!;
        public virtual DbSet<ResultsJsonTest> ResultsJsonTests { get; set; } = null!;
        public virtual DbSet<ResultsJsonValue> ResultsJsonValues { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
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
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HOFMANN_STAT;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Cyrillic_General_100_CS_AS_KS");

            modelBuilder.Entity<Mes2supPath>(entity =>
            {
                entity.ToTable("mes2sup_paths");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CLogin)
                    .HasMaxLength(50)
                    .HasColumnName("c_login");

                entity.Property(e => e.CPassword)
                    .HasMaxLength(50)
                    .HasColumnName("c_password");

                entity.Property(e => e.CPath)
                    .HasMaxLength(255)
                    .HasColumnName("c_path");

                entity.Property(e => e.StandId).HasColumnName("stand_id");

                entity.HasOne(d => d.Stand)
                    .WithMany(p => p.Mes2supPaths)
                    .HasForeignKey(d => d.StandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_mes2sup_paths_stands");
            });

            modelBuilder.Entity<Mes2supTelegram>(entity =>
            {
                entity.ToTable("mes2sup_telegrams");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Ordernum)
                    .HasMaxLength(20)
                    .HasColumnName("ordernum");

                entity.Property(e => e.TgContent)
                    .HasColumnType("xml")
                    .HasColumnName("tg_content");

                entity.Property(e => e.TgFilename)
                    .HasMaxLength(255)
                    .HasColumnName("tg_filename");

                entity.Property(e => e.Vin)
                    .HasMaxLength(17)
                    .HasColumnName("vin");
            });

            modelBuilder.Entity<Mes2supTelegramsStand>(entity =>
            {
                entity.ToTable("mes2sup_telegrams_stands");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.StandId).HasColumnName("stand_id");

                entity.Property(e => e.TgId).HasColumnName("tg_id");

                entity.Property(e => e.Transfered)
                    .HasColumnType("date")
                    .HasColumnName("transfered")
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

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Val)
                    .HasMaxLength(3)
                    .HasColumnName("val");
            });

            modelBuilder.Entity<Operator>(entity =>
            {
                entity.ToTable("operators");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InactiveMark)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("inactive_mark")
                    .HasDefaultValueSql("('FALSE')");

                entity.Property(e => e.ODescription)
                    .HasMaxLength(1024)
                    .HasColumnName("o_description");

                entity.Property(e => e.OLogin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("o_login");

                entity.Property(e => e.OPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("o_password");
            });

            modelBuilder.Entity<OperatorsStand>(entity =>
            {
                entity.ToTable("operators_stands");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OperatorId).HasColumnName("operator_id");

                entity.Property(e => e.StandId).HasColumnName("stand_id");

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

                entity.Property(e => e.ID).HasColumnName("id");

                entity.Property(e => e.OSVersion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("os_ver");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.HasKey(e => e.PName)
                    .HasName("PK_PICTURES");

                entity.ToTable("pictures");

                entity.Property(e => e.PName)
                    .HasMaxLength(255)
                    .HasColumnName("p_name");

                entity.Property(e => e.Picture1)
                    .HasColumnType("image")
                    .HasColumnName("picture");
            });

            modelBuilder.Entity<PicturesPath>(entity =>
            {
                entity.ToTable("pictures_paths");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CLogin)
                    .HasMaxLength(50)
                    .HasColumnName("c_login");

                entity.Property(e => e.CPassword)
                    .HasMaxLength(50)
                    .HasColumnName("c_password");

                entity.Property(e => e.CPath)
                    .HasMaxLength(255)
                    .HasColumnName("c_path");

                entity.Property(e => e.StandId).HasColumnName("stand_id");

                entity.HasOne(d => d.Stand)
                    .WithMany(p => p.PicturesPaths)
                    .HasForeignKey(d => d.StandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pictures_paths_stands");
            });

            modelBuilder.Entity<ResultsJsonHeader>(entity =>
            {
                entity.ToTable("results_json_headers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.JsonFilename)
                    .HasMaxLength(255)
                    .HasColumnName("json_filename");

                entity.Property(e => e.OperatorId).HasColumnName("operator_id");

                entity.Property(e => e.Ordernum)
                    .HasMaxLength(20)
                    .HasColumnName("ordernum");

                entity.Property(e => e.StandId).HasColumnName("stand_id");

                entity.Property(e => e.VIN)
                    .HasMaxLength(17)
                    .HasColumnName("vin");

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

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.HeaderId).HasColumnName("header_id");

                entity.Property(e => e.ResId).HasColumnName("res_id");

                entity.Property(e => e.TName)
                    .HasMaxLength(255)
                    .HasColumnName("t_name");

                entity.Property(e => e.TSpecname)
                    .HasMaxLength(50)
                    .HasColumnName("t_specname");

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

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.Property(e => e.VName)
                    .HasMaxLength(100)
                    .HasColumnName("v_name");

                entity.Property(e => e.VValue)
                    .HasMaxLength(50)
                    .HasColumnName("v_value");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.ResultsJsonValues)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_results_json_values_results_json_tests");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RDescription)
                    .HasMaxLength(255)
                    .HasColumnName("r_description");

                entity.Property(e => e.RName)
                    .HasMaxLength(50)
                    .HasColumnName("r_name")
                    .HasDefaultValueSql("('user')");
            });

            modelBuilder.Entity<Stand>(entity =>
            {
                entity.ToTable("stands");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DnsName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dns_name");

                entity.Property(e => e.InactiveMark)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("inactive_mark")
                    .HasDefaultValueSql("('FALSE')");

                entity.Property(e => e.IpAdress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ip_adress");

                entity.Property(e => e.OSVersionNavigationID).HasColumnName("os_version");

                entity.Property(e => e.Placement)
                    .HasMaxLength(50)
                    .HasColumnName("placement");

                entity.Property(e => e.Project)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("project")
                    .HasDefaultValueSql("('HOFMANN')");

                entity.Property(e => e.StandName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("stand_name");

                entity.Property(e => e.StandNameDescription)
                    .HasMaxLength(255)
                    .HasColumnName("stand_name_description");

                entity.Property(e => e.StandType)
                    .HasMaxLength(50)
                    .HasColumnName("stand_type")
                    .HasDefaultValueSql("('UNKNOWN')");

                entity.Property(e => e.WorkplaceMes)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("workplace_mes");

                entity.HasOne(d => d.OsVersionNavigation)
                    .WithMany(p => p.Stands)
                    .HasForeignKey(d => d.OSVersionNavigationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_stands_os_versions");
            });

            modelBuilder.Entity<Sup2mesPath>(entity =>
            {
                entity.ToTable("sup2mes_paths");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CLogin)
                    .HasMaxLength(50)
                    .HasColumnName("c_login");

                entity.Property(e => e.CPassword)
                    .HasMaxLength(50)
                    .HasColumnName("c_password");

                entity.Property(e => e.CPath)
                    .HasMaxLength(255)
                    .HasColumnName("c_path");

                entity.Property(e => e.StandId).HasColumnName("stand_id");

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

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Ordernum)
                    .HasMaxLength(20)
                    .HasColumnName("ordernum");

                entity.Property(e => e.StandId).HasColumnName("stand_id");

                entity.Property(e => e.TgContent)
                    .HasColumnType("xml")
                    .HasColumnName("tg_content");

                entity.Property(e => e.TgFilename)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tg_filename");

                entity.Property(e => e.Vin)
                    .HasMaxLength(17)
                    .HasColumnName("vin");

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

                entity.Property(e => e.EngVariant)
                    .HasMaxLength(255)
                    .HasColumnName("eng_variant");

                entity.Property(e => e.RusVariant)
                    .HasMaxLength(255)
                    .HasColumnName("rus_variant");
            });

            modelBuilder.Entity<TranslatesPath>(entity =>
            {
                entity.ToTable("translates_paths");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CLogin)
                    .HasMaxLength(50)
                    .HasColumnName("c_login");

                entity.Property(e => e.CPassword)
                    .HasMaxLength(50)
                    .HasColumnName("c_password");

                entity.Property(e => e.CPath)
                    .HasMaxLength(255)
                    .HasColumnName("c_path");

                entity.Property(e => e.StandId).HasColumnName("stand_id");

                entity.HasOne(d => d.Stand)
                    .WithMany(p => p.TranslatesPaths)
                    .HasForeignKey(d => d.StandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_translates_paths_stands");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InactiveMark)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("inactive_mark")
                    .HasDefaultValueSql("('FALSE')");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.ULogin)
                    .HasMaxLength(50)
                    .HasColumnName("u_login");

                entity.Property(e => e.UPassword)
                    .HasMaxLength(50)
                    .HasColumnName("u_password");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_users_roles");
            });

            modelBuilder.Entity<XsdSchema>(entity =>
            {
                entity.ToTable("xsd_schemas");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PurposeId).HasColumnName("purpose_id");

                entity.Property(e => e.XsdDescription)
                    .HasMaxLength(255)
                    .HasColumnName("xsd_description");

                entity.Property(e => e.XsdSchema1).HasColumnName("xsd_schema");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.XsdSchemas)
                    .HasForeignKey(d => d.PurposeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_xsd_schemas_xsd_schemas_purpose");
            });

            modelBuilder.Entity<XsdSchemasPurpose>(entity =>
            {
                entity.ToTable("xsd_schemas_purpose");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.XsdPurpose)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("xsd_purpose");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
