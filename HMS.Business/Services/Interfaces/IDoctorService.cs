using HMS.Core.Entities;

namespace HMS.Business.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<List<Doctor>> GetAllAsync();
        Task<List<Doctor>> GetPatientDoctorsAsync(string patientId);
        Task CreateAsync();
    }
}