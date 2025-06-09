using Microsoft.EntityFrameworkCore;
using PatientHeartRateService.Models;

namespace PatientHeartRateService.Data
{
    public class HeartRateContext : DbContext
    {
        public HeartRateContext(DbContextOptions<HeartRateContext> options) : base(options) { }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<HeartRateReading> HeartRateReadings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Patient entity
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Gender).IsRequired();
            });

            // Configure HeartRateReading entity
            modelBuilder.Entity<HeartRateReading>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(e => e.Patient)
                      .WithMany(p => p.HeartRateReadings)
                      .HasForeignKey(e => e.PatientId);
            });
        }
    }
}
