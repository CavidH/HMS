using HMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Data.ModelConfiguration
{
    class DoctorPatientConfig : IEntityTypeConfiguration<DoctorPatient>
    {
        public void Configure(EntityTypeBuilder<DoctorPatient> builder)
        {
        builder.Property(p => p.IsDeleted).HasDefaultValue(false);


            builder.HasOne(p => p.Doctor)
                .WithMany(dp => dp.DoctorPatients)
                .HasForeignKey(dp => dp.DoctorId)
                 .OnDelete(DeleteBehavior.ClientSetNull);



            builder.HasOne(p => p.Patient)
                .WithMany(cd => cd.PatientDoctor)
                .HasForeignKey(ci => ci.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //builder.Property(p => p.IsDeleted).HasDefaultValue(false);
        }
    }
}