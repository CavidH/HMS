using HMS.Core.Entities;
using HMS.Core.Enums;
using HMS.Data.ModelConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HMS.Data.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //add-migration Mig -o DAL/Migrations
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Configuration

            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new DoctorPatientConfig());
            //builder.ApplyConfiguration(new SpecialityConfig());
            // builder.ApplyConfiguration(new DoctorSpecialityConfig());

            #endregion

            base.OnModelCreating(builder);

            #region Roles

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "4f93cf95-d9dc-49ee-9cd2-411c309eafe5",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "7ebf1dce-81b3-48af-8c4a-9cb1b7280f50"
                }, new IdentityRole()
                {
                    Id = "30861f84-9bdd-4178-bb53-371883e974d6",
                    Name = "Doctor",
                    NormalizedName = "DOCTOR",
                    ConcurrencyStamp = "b7376b7e-2649-4964-8373-78bd8ff3440a"
                }, new IdentityRole()
                {
                    Id = "0334e6df-aa49-4cea-9435-1866976b392a",
                    Name = "Patient",
                    NormalizedName = "PATIENT",
                    ConcurrencyStamp = "7800b43b-5533-484c-8be2-995e7cd9ba21"
                }
            );

            #endregion

            #region Admin

            //admin@gmail.com 123456
            builder.Entity<AppUser>().HasData(
                new AppUser()
                {
                    Id = "87c397d1-6f89-49b0-a382-8a8896edee22",
                    FirstName = "admin",
                    LastName = "admin",
                    Gender = Gender.Male,
                    BloodGroup = BloodGroup.ANegative,
                    IsActivated = true,
                    CreatedAt = DateTime.Now,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEK+z5KG4X5Hfrv8tqswSGuHPqho4j4mqNr+foUao8RgTKBbrXEOzN1J9J+M3aGEV8w==",
                    SecurityStamp = "GK75FVQWVXNYSY76CMXAWR6TWYXOJHD4",
                    ConcurrencyStamp = "55b5d348-6f0b-4d50-a270-83163225d083",
                    PhoneNumber = "123456789",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            );

            #endregion
        }

        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<DoctorPatient> DoctorPatient { get; set; }

        //public DbSet<DoctorPatient> DoctorPatients { get; set; }
        //public DbSet<Speciality> Speciality { get; set; }
        //public DbSet<DoctorSpeciality> DoctorSpecialities { get; set; }
    }
}