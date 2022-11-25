namespace HMS.Core.Abstracts
{
    public interface IUnitOfWork
    {
        public IDoctorRepository DoctorRepository { get; }
        public IPatientRepository PatientRepository { get; }
        public IDoctorPatientRepository DoctorPatientRepository { get; }

        Task SaveAsync();

    }
}
