using HMS.Core.Entities;

namespace HMS.Core.Abstracts
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<IEnumerable<Patient>> GetDoctorPatientsAsync(string docId);
    }
}