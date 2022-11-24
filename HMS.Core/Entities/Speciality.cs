namespace HMS.Core.Entities
{
    public class Speciality
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        //Navigation Property
        public ICollection<DoctorSpeciality> DoctorSpecialities { get; set; }
    }
}
