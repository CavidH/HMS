using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Core.Entities
{
    public class DoctorPatient
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.Now;
        public bool IsDeleted { get; set; }

        //Navigation Property
        public string DoctorId { get; set; }
        public AppUser Doctor { get; set; }

        public string PatientId { get; set; }
        public AppUser Patient { get; set; }

    }
}
