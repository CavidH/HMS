using HMS.Business.Services.Interfaces;
using HMS.Business.ViewModels;
using HMS.Core.Abstracts;
using HMS.Core.Entities;

namespace HMS.Business.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Doctor>> GetAllAsync() =>
            await _unitOfWork.DoctorRepository.GetAllAsync(p => p.AppUser.IsDeleted == false, "AppUser");

        public async Task<List<Doctor>> GetPatientDoctorsAsync(string patientId) =>
            await _unitOfWork.DoctorRepository.GetPatientDoctorsAsync(patientId);


        // {
        // List<Doctor> doctors = await _unitOfWork.DoctorRepository.GetAllPaginatedAsync(page, 5, null, "AppUser");
        // return new Paginate<List<Doctor>>() { Items = doctors, CurrentPage = page, PageCount = 5,AllPageCount = 45};
        // }

        public Task CreateAsync()
        {
            throw new NotImplementedException();
        }
    }
}