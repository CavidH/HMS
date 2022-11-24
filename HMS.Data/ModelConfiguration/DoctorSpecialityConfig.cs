//using HMS.Core.Entities;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore;

//namespace HMS.Data.ModelConfiguration
//{
//    class DoctorSpecialityConfig : IEntityTypeConfiguration<DoctorSpeciality>
//    {
//        public void Configure(EntityTypeBuilder<DoctorSpeciality> builder)
//        {
//            builder.HasKey(x => new { x.SpecialityId, x.DoctorId });

//            builder.HasOne(p => p.Doctor)
//                .WithMany(dp => dp.DoctorSpecialities)
//                .HasForeignKey(dp => dp.DoctorId);



//            builder.HasOne(p => p.Speciality)
//                .WithMany(cd => cd.DoctorSpecialities)
//                .HasForeignKey(ci => ci.SpecialityId);

//            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
//        }
//    }
//}
