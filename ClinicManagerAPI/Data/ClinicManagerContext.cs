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
    }
}
