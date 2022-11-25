using HMS.Core.Abstracts;
using HMS.Data.DAL;
using HMS.Data.Repositories.Implementations;

namespace HMS.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IDoctorRepository _doctorRepository;
        private IPatientRepository _patientRepository;
        private IDoctorPatientRepository _doctorPatientRepository;


        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDoctorRepository DoctorRepository => _doctorRepository ??= new DoctorRepository(_dbContext);

        public IPatientRepository PatientRepository => _patientRepository ??= new PatientRepository(_dbContext);

        public IDoctorPatientRepository DoctorPatientRepository => _doctorPatientRepository ??= new DoctorPatientRepository(_dbContext);

        public async Task SaveAsync() => _dbContext.SaveChangesAsync();
    }
}