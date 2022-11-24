using HMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMS.Data.ModelConfiguration
{
    class DoctorPatientConfig : IEntityTypeConfiguration<DoctorPatient>
    {
        public void Configure(EntityTypeBuilder<DoctorPatient> builder)
        {
             builder.HasKey(x => new { x.PatientId, x.DoctorId });

            builder.HasOne(p => p.Doctor)
                .WithMany(dp => dp.DoctorPatients)
                .HasForeignKey(dp => dp.DoctorId)
                 .OnDelete(DeleteBehavior.ClientSetNull);



            builder.HasOne(p => p.Patient)
                .WithMany(cd => cd.PatientDoctors)
                .HasForeignKey(ci => ci.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
        }
    }
}