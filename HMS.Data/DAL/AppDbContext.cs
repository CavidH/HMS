using HMS.Core.Entities;
using HMS.Data.ModelConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HMS.Data.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
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
        }

        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Patient> Patient { get; set; }
       public DbSet<DoctorPatient> DoctorPatient { get; set; }

        //public DbSet<DoctorPatient> DoctorPatients { get; set; }
        //public DbSet<Speciality> Speciality { get; set; }
        //public DbSet<DoctorSpeciality> DoctorSpecialities { get; set; }
    }
}
