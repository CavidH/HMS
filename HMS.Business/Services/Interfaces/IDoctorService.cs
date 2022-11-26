using HMS.Core.Entities;

namespace HMS.Business.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor> GetByIdAsync(int id);
        Task<List<Doctor>> GetPatientDoctorsAsync(string patientId);

        Task RemoveAsync(string userId, int doctorId);
        //     Task CreateAsync();
    }
}