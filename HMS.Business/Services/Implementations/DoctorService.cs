using HMS.Business.Exceptions;
using HMS.Business.Services.Interfaces;
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

        public async Task<Doctor> GetByIdAsync(int id)
        {
            Doctor doctor = await _unitOfWork.DoctorRepository.GetAsync(p => p.Id == id,"AppUser");
            if (doctor is null)
                throw new UserNotFoundException("User Not Found id:" + id);
            return doctor;
        }

        public async Task<List<Doctor>> GetPatientDoctorsAsync(string patientId) =>
            await _unitOfWork.DoctorRepository.GetPatientDoctorsAsync(patientId);


        // {
        // List<Doctor> doctors = await _unitOfWork.DoctorRepository.GetAllPaginatedAsync(page, 5, null, "AppUser");
        // return new Paginate<List<Doctor>>() { Items = doctors, CurrentPage = page, PageCount = 5,AllPageCount = 45};
        // }


        public async Task RemoveAsync(string userId, int doctorId)
        {
            Patient patient = await _unitOfWork.PatientRepository.GetAsync(p => p.AppUserId == userId);

            DoctorPatient doctorPatient = await _unitOfWork.DoctorPatientRepository
                .GetAsync(p => p.DoctorId == doctorId && p.PatientId == patient.Id && p.IsDeleted == false);
            doctorPatient.IsDeleted = true;
            await _unitOfWork.SaveAsync();
        }
    }
}