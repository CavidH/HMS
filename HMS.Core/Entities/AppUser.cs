using Microsoft.AspNetCore.Identity;

namespace HMS.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActivated { get; set; }
        public DateTime CreatedAt { get; set; }



        //Navigation Property

        public ICollection<DoctorPatient> DoctorPatients { get; set; }
        public ICollection<DoctorPatient> PatientDoctors { get; set; }
        public ICollection<DoctorSpeciality> DoctorSpecialities { get; set; }


    }
}
