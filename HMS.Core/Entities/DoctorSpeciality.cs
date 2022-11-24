
namespace HMS.Core.Entities
{
    public class DoctorSpeciality
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

        //Navigation Property
        public string DoctorId { get; set; }
        public AppUser Doctor { get; set; }

        public int SpecialityId { get; set; }
        public Speciality Speciality { get; set; }
    }
}
