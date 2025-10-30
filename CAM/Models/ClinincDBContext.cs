using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CAM.Models;

public class ClinicDbContext : DbContext
{
    public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options) { }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
            .Property(a => a.RowVersion).IsRowVersion();

        modelBuilder.Entity<Appointment>()
            .HasIndex(a => new { a.DoctorId, a.StartAt }).IsUnique(false);
    }
}

