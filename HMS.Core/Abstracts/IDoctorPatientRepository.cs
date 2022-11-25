using HMS.Core.Entities;

namespace HMS.Core.Abstracts
{
    public interface IDoctorPatientRepository : IRepository<DoctorPatient>
    {
        Task<bool> IsExistAsync(int patientId, int doctorId);
    }
}