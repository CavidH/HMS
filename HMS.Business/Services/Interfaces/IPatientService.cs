using HMS.Core.Entities;

namespace HMS.Business.Services.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetDoctorPatientsAsync(string docId);
        Task RemoveAsync(string userId, int patientId);

    }
}