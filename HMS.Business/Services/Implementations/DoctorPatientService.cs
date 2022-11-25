using HMS.Business.Services.Interfaces;
using HMS.Core.Abstracts;
using HMS.Core.Entities;

namespace HMS.Business.Services.Implementations
{
    public class DoctorPatientService : IDoctorPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorPatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(string userId, int doctorId)
        {
            Patient patient = await _unitOfWork.PatientRepository.GetAsync(p => p.AppUserId == userId);
            await _unitOfWork.DoctorPatientRepository.CreateAsync(new DoctorPatient()
                { PatientId = patient.Id, DoctorId = doctorId });
            await _unitOfWork.SaveAsync();
        }
    }
}