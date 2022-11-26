using HMS.Business.Services.Interfaces;
using HMS.Core.Abstracts;
using HMS.Core.Entities;

namespace HMS.Business.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Patient>> GetDoctorPatientsAsync(string docId) =>
            await _unitOfWork.PatientRepository.GetDoctorPatientsAsync(docId);

        public async Task RemoveAsync(string userId, int patientId)
        {
            Doctor doctor = await _unitOfWork.DoctorRepository.GetAsync(p => p.AppUserId == userId);

            DoctorPatient doctorPatient = await _unitOfWork.DoctorPatientRepository
                .GetAsync(p => p.DoctorId == doctor.Id && p.PatientId == patientId && p.IsDeleted == false);
            doctorPatient.IsDeleted = true;
            await _unitOfWork.SaveAsync();
        }
    }
}