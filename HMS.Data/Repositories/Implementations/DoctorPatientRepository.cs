using HMS.Core.Abstracts;
using HMS.Core.Entities;
using HMS.Data.DAL;

namespace HMS.Data.Repositories.Implementations
{
    public class DoctorPatientRepository : Repository<DoctorPatient>, IDoctorPatientRepository
    {
        public DoctorPatientRepository(AppDbContext context) : base(context)
        {
        }
    }
}
