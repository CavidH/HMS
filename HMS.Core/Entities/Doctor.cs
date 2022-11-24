namespace HMS.Core.Entities
{
    public class Doctor
    {
        public int Id { get; set; }



        //Navigation Property
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<DoctorPatient> DoctorPatients { get; set; }
    }
}
