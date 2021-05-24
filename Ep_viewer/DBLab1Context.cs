using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Ep_viewer
{
    public partial class DBLab1Context : DbContext
    {
        public DBLab1Context()
        {
        }

        public DBLab1Context(DbContextOptions<DBLab1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Episode> Episodes { get; set; }
        public virtual DbSet<EpisodeDefinition> EpisodeDefinitions { get; set; }
        public virtual DbSet<Hospital> Hospitals { get; set; }
        public virtual DbSet<Insurance> Insurances { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= DESKTOP-P44AB47; Database=DBLab1; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.HospitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctors_Hospitals");
            });

            modelBuilder.Entity<Episode>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Info)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Payment).HasColumnType("money");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Episodes)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Episodes_Doctors");

                entity.HasOne(d => d.EpisodeDefinitionNavigation)
                    .WithMany(p => p.Episodes)
                    .HasForeignKey(d => d.EpisodeDefinition)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Episodes_Episode_Definition");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Episodes)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Episodes_Patients");
            });

            modelBuilder.Entity<EpisodeDefinition>(entity =>
            {
                entity.ToTable("Episode_Definition");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Definition)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnType("ntext");
            });

            modelBuilder.Entity<Insurance>(entity =>
            {
                entity.ToTable("Insurance");

                entity.Property(e => e.Policy).HasColumnType("ntext");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Insurances)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Insurance_Patients");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
