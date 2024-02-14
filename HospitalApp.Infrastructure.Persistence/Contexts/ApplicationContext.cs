using System;
using HospitalApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Infrastructure.Persistence.Contexts
{
	public class ApplicationContext : DbContext

    {
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<LabResult> LabResults { get; set; }

        // constructor
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluent API
            base.OnModelCreating(modelBuilder);

            #region tables
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<LabTest>().ToTable("LabTests");
            modelBuilder.Entity<LabResult>().ToTable("LabResults");
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
            #endregion

            #region primary-keys
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<Patient>().HasKey(patient => patient.Id);
            modelBuilder.Entity<LabTest>().HasKey(labTest => labTest.Id);
            modelBuilder.Entity<LabResult>().HasKey(labResult => labResult.Id);
            modelBuilder.Entity<Doctor>().HasKey(doctor => doctor.Id);
            modelBuilder.Entity<Appointment>().HasKey(appointment => appointment.Id);
            #endregion

            #region relationships
            modelBuilder.Entity<Doctor>()
               .HasMany(d => d.Appointments)
               .WithOne(a => a.Doctor)
               .HasForeignKey(a => a.DoctorId)
               .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.LabResults)
                .WithOne(l => l.Patient)
                .HasForeignKey(l => l.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LabTest>()
                .HasMany(lt => lt.LabResults)
                .WithOne(lr => lr.LabTest)
                .HasForeignKey(lr => lr.LabTestId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region property configurations

                #region doctor
                modelBuilder.Entity<Doctor>(entity =>
                {
                    entity.Property(d => d.FirstName).IsRequired();
                    entity.Property(d => d.LastName).IsRequired();
                    entity.Property(d => d.Email).IsRequired();
                    entity.Property(d => d.PhoneNumber).IsRequired();
                    entity.Property(d => d.IdentificationNumber).IsRequired();
                    entity.Property(d => d.Photo).IsRequired();
                });
                #endregion

                #region appointment
                modelBuilder.Entity<Appointment>(entity =>
                {
                    entity.Property(a => a.Date).IsRequired();
                    entity.Property(a => a.Time).IsRequired();
                    entity.Property(a => a.Reason).IsRequired();
                });
                #endregion

                #region LabResult
                modelBuilder.Entity<LabResult>(entity =>
                {
                    entity.Property(lr => lr.Result).IsRequired();
                    entity.Property(lr => lr.Status).IsRequired();
                });
                #endregion

                #region LabTest
                modelBuilder.Entity<LabTest>(entity =>
                {
                    entity.Property(lt => lt.Name).IsRequired();
                });
                #endregion

                #region Patient
                modelBuilder.Entity<Patient>(entity =>
                {
                    entity.Property(p => p.FirstName).IsRequired();
                    entity.Property(p => p.LastName).IsRequired();
                    entity.Property(p => p.PhoneNumber).IsRequired();
                    entity.Property(p => p.Address).IsRequired();
                    entity.Property(p => p.IdentificationNumber).IsRequired();
                    entity.Property(p => p.DateOfBirth).IsRequired();
                    entity.Property(p => p.IsSmoker).IsRequired();
                    entity.Property(p => p.HasAllergies).IsRequired();
                    entity.Property(p => p.Photo).IsRequired();
                });
                 #endregion

                #region user
                modelBuilder.Entity<User>(entity =>
                {
                    entity.Property(u => u.FirstName).IsRequired();
                    entity.Property(u => u.LastName).IsRequired();
                    entity.Property(u => u.Email).IsRequired();
                    entity.Property(u => u.Username).IsRequired();
                    entity.Property(u => u.Password).IsRequired();
                    entity.Property(u => u.UserType).IsRequired();
                });
                #endregion

            #endregion
        }

    }
}

