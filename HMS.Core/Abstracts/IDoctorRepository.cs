using HMS.Core.Entities;

namespace HMS.Core.Abstracts
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<List<Doctor>> GetPatientDoctorsAsync(string patientId);
    }
}