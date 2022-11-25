using HMS.Core.Abstracts;
using HMS.Core.Entities;
using HMS.Data.DAL;

namespace HMS.Data.Repositories.Implementations
{

    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(AppDbContext context) : base(context) { }

    }
}
