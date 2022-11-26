using HMS.Business.Exceptions;
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
            bool isExist = await _unitOfWork.DoctorPatientRepository.IsExistAsync(patient.Id, doctorId);
            if (isExist) throw new DoctorPatientException("Already Available");

            await _unitOfWork.DoctorPatientRepository.CreateAsync(new DoctorPatient()
                { PatientId = patient.Id, DoctorId = doctorId });
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(string userId, int doctorId)
        {
            Patient patient = await _unitOfWork.PatientRepository.GetAsync(p => p.AppUserId == userId);

            DoctorPatient doctorPatient = await _unitOfWork.DoctorPatientRepository
                .GetAsync(p => p.DoctorId == doctorId && p.PatientId == patient.Id&&p.IsDeleted==false);
            doctorPatient.IsDeleted = true; 
           await _unitOfWork.SaveAsync();
        }
    }
}