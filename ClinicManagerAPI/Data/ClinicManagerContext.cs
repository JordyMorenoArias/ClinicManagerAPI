using ClinicManagerAPI.Models.DTOs.DoctorProfile;
using ClinicManagerAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagerAPI.Data
{
    public class ClinicManagerContext : DbContext
    {
        public ClinicManagerContext(DbContextOptions<ClinicManagerContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<AppointmentEntity> Appointments { get; set; } = null!;
        public DbSet<PatientEntity> Patients { get; set; } = null!;
        public DbSet<MedicalRecordEntity> MedicalRecords { get; set; } = null!;
        public DbSet<DoctorProfileEntity> DoctorProfiles { get; set; } = null!;
        public DbSet<AllergyEntity> Allergies { get; set; } = null!;
        public DbSet<PatientAllergyEntity> PatientAllergies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<PatientAllergyEntity>()
                .HasIndex(pa => new { pa.PatientId, pa.AllergyId })
                .IsUnique();

            modelBuilder.Entity<PatientAllergyEntity>()
                .HasOne(pa => pa.Patient)
                .WithMany(p => p.Allergies)
                .HasForeignKey(pa => pa.PatientId);

            modelBuilder.Entity<PatientAllergyEntity>()
                .HasOne(pa => pa.Allergy)
                .WithMany(a => a.Patients)
                .HasForeignKey(pa => pa.AllergyId);
        }
    }
}