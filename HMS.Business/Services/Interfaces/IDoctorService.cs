using HMS.Business.ViewModels;
using HMS.Core.Entities;

namespace HMS.Business.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<List<Doctor>> GetAllAsync( );
        Task CreateAsync();
    }
}
