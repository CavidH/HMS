
using System.ComponentModel.DataAnnotations;

namespace HMS.Core.Entities
{
    public class DoctorPatient
    {
        [Key]
        public int Id{ get; set; }

        public bool IsDeleted { get; set; }




        //Navigation Property
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
