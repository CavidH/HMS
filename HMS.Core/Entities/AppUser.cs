using HMS.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace HMS.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Detail { get; set; }
        public Gender Gender { get; set; }
        public BloodGroup BloodGroup { get; set; }

        public bool IsActivated { get; set; }
        public DateTime CreatedAt { get; set; }

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }



        //Navigation Property

        //public ICollection<DoctorPatient> DoctorPatients { get; set; }
        //public ICollection<DoctorPatient> PatientDoctors { get; set; }
        //public ICollection<DoctorSpeciality> DoctorSpecialities { get; set; }


    }
}
