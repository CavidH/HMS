using HMS.Business.Services.Implementations;

namespace HMS.Business.Services.Interfaces
{
    public interface IUnitOfWorkService
    {
        public IUserService UserService { get; }
        public IDoctorService DoctorService { get; }
        public IDoctorPatientService DoctorPatientService { get; }
        public IPatientService PatientService { get; }
    }
}
