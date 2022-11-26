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

            #region Users

            //admin@gmail.com 123456
            // builder.Entity<AppUser>().HasData(
            //     new AppUser()
            //     {
            //         Id = "87c397d1-6f89-49b0-a382-8a8896edee22",
            //         FirstName = "admin",
            //         LastName = "admin",
            //         Gender = Gender.Male,
            //         BloodGroup = BloodGroup.ANegative,
            //         IsActivated = true,
            //         CreatedAt = DateTime.Now,
            //         UserName = "admin",
            //         NormalizedUserName = "ADMIN",
            //         Email = "admin@gmail.com",
            //         NormalizedEmail = "ADMIN@GMAIL.COM",
            //         EmailConfirmed = false,
            //         PasswordHash =
            //             "AQAAAAEAACcQAAAAEK+z5KG4X5Hfrv8tqswSGuHPqho4j4mqNr+foUao8RgTKBbrXEOzN1J9J+M3aGEV8w==",
            //         SecurityStamp = "GK75FVQWVXNYSY76CMXAWR6TWYXOJHD4",
            //         ConcurrencyStamp = "55b5d348-6f0b-4d50-a270-83163225d083",
            //         PhoneNumber = "123456789",
            //         PhoneNumberConfirmed = false,
            //         TwoFactorEnabled = false,
            //         LockoutEnabled = true,
            //         AccessFailedCount = 0
            //     }, new AppUser()
            //     {
            //         Id = "475f14f1-3342-4dc6-a312-3cf9b6fb5360",
            //         FirstName = "Doctor",
            //         LastName = "Doctorr",
            //         Detail = "I am Doctor",
            //         Gender = Gender.Male,
            //         BloodGroup = BloodGroup.OPositive,
            //         IsActivated = true,
            //         CreatedAt = DateTime.Now,
            //         UserName = "Doctor123",
            //         NormalizedUserName = "DOCTOR123",
            //         Email = "doctor@gmail.com",
            //         NormalizedEmail = "DOCTOR@GMAIL.COM",
            //         EmailConfirmed = false,
            //         PasswordHash =
            //             "AQAAAAEAACcQAAAAEB3vDbFtRY3Mwq6ZidWlr72OQ63IQc/PzI4e7e8H2kvsD8XmbI7b2Vh7gBFFLLpX4Q==",
            //         SecurityStamp = "NAMGIGQIGC2XCEOH3M6Y4T7ZG7525U2N",
            //         ConcurrencyStamp = "3eed2c58-efe1-4c7e-89eb-3da4949402c0",
            //         PhoneNumber = "+12345678",
            //         PhoneNumberConfirmed = false,
            //         TwoFactorEnabled = false,
            //         LockoutEnabled = true,
            //         AccessFailedCount = 0
            //     }, new AppUser()
            //     {
            //         Id = "b4e50309-81e7-44c5-ad37-8cec4f95ca2c",
            //         FirstName = "Patient2",
            //         LastName = "Patient2",
            //         Detail = "I am  Patient2",
            //         Gender = Gender.Male,
            //         BloodGroup = BloodGroup.OPositive,
            //         IsActivated = true,
            //         CreatedAt = DateTime.Now,
            //         UserName = "SecondPatient",
            //         NormalizedUserName = "SECONDPATIENT",
            //         Email = "Patient2@gmail.com",
            //         NormalizedEmail = "PATIENT2@GMAIL.COM",
            //         EmailConfirmed = false,
            //         PasswordHash =
            //             "AQAAAAEAACcQAAAAEKklZkqNMJH66S20ZiTVzih812z2cvQldXJfilNtRhI//xDrt3AI3RXK2SkFSaAMPA==",
            //         SecurityStamp = "XJZFCQTWS2EE435RBL4JNXYANHPBL7O5",
            //         ConcurrencyStamp = "1a8e8fc2-989b-42d1-82cf-e2d4a823635b",
            //         PhoneNumber = "+133515315",
            //         PhoneNumberConfirmed = false,
            //         TwoFactorEnabled = false,
            //         LockoutEnabled = true,
            //         AccessFailedCount = 0
            //     },new AppUser()
            //     {
            //         Id = "d13faee7-cf52-4b71-9e95-b7edec384c53",
            //         FirstName = "Patient1",
            //         LastName = "Patient1",
            //         Detail = "I Am Patient",
            //         Gender = Gender.Male,
            //         BloodGroup = BloodGroup.BNegative,
            //         IsActivated = true,
            //         CreatedAt = DateTime.Now,
            //         UserName = "Patient",
            //         NormalizedUserName = "PATIENT",
            //         Email = "Patient@gmail.com",
            //         NormalizedEmail = "PATIENT@GMAIL.COM",
            //         EmailConfirmed = false,
            //         PasswordHash =
            //             "AQAAAAEAACcQAAAAELF9Kd2EDKMhn6DoYkFzR6/cYzKOGhcQyJBxKWHfn5Tpf9Ax53A8XfLplwBNpmduxQ==",
            //         SecurityStamp = "A7ZKKU2XMUVNXP5UJKHYFWMO5E2HORZL",
            //         ConcurrencyStamp = "c67b1a09-d305-4e7b-b9e7-a7a080c6ede7",
            //         PhoneNumber = "+123456456",
            //         PhoneNumberConfirmed = false,
            //         TwoFactorEnabled = false,
            //         LockoutEnabled = true,
            //         AccessFailedCount = 0
            //     },new AppUser()
            //     {
            //         Id = "f13b277b-492a-429b-ba5d-f98a3d18b71d",
            //         FirstName = "doctor2",
            //         LastName = "doctor2",
            //         Detail = "I am doctor 2",
            //         Gender = Gender.Female,
            //         BloodGroup = BloodGroup.ABPositive,
            //         IsActivated = true,
            //         CreatedAt = DateTime.Now,
            //         UserName = "seconddoctor",
            //         NormalizedUserName = "SECONDDOCTOR",
            //         Email = "doctor2@gmail.com",
            //         NormalizedEmail = "DOCTOR2@GMAIL.COM",
            //         EmailConfirmed = false,
            //         PasswordHash =
            //             "AQAAAAEAACcQAAAAEE8EOymicoeZ3wfSCef0iOuJoNVEeAkXoAbog/JzKS9033V6Vf63GzE5GWRxh6EsQA==",
            //         SecurityStamp = "RHBZX43DDSDYTEAEFSDCLAZN2LSPKCX4",
            //         ConcurrencyStamp = "f3f558b3-c7a2-4213-be05-3671251844de",
            //         PhoneNumber = "+56565656",
            //         PhoneNumberConfirmed = false,
            //         TwoFactorEnabled = false,
            //         LockoutEnabled = true,
            //         AccessFailedCount = 0
            //     }
            // );

            #endregion

            #region UserRoles

            // builder.Entity<IdentityUserRole<string>>().HasData(
            //     new IdentityUserRole<string>()
            //     {
            //         RoleId = "0334e6df-aa49-4cea-9435-1866976b392a",
            //         UserId = "b4e50309-81e7-44c5-ad37-8cec4f95ca2c"
            //     },
            //     new IdentityUserRole<string>()
            //     {
            //         RoleId = "0334e6df-aa49-4cea-9435-1866976b392a",
            //         UserId = "d13faee7-cf52-4b71-9e95-b7edec384c53"
            //     },
            //     new IdentityUserRole<string>()
            //     {
            //         RoleId = "30861f84-9bdd-4178-bb53-371883e974d6",
            //         UserId = "475f14f1-3342-4dc6-a312-3cf9b6fb5360"
            //     },
            //     new IdentityUserRole<string>()
            //     {
            //         RoleId = "30861f84-9bdd-4178-bb53-371883e974d6",
            //         UserId = "f13b277b-492a-429b-ba5d-f98a3d18b71d"
            //     },
            //     new IdentityUserRole<string>()
            //     {
            //         RoleId = "4f93cf95-d9dc-49ee-9cd2-411c309eafe5",
            //         UserId = "87c397d1-6f89-49b0-a382-8a8896edee22"
            //     }
            // );

            #endregion
        }

        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<DoctorPatient> DoctorPatient { get; set; }
    }
}