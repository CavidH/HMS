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
    }
}