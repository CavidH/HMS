using HMS.Core.Abstracts;
using HMS.Core.Entities;
using HMS.Data.DAL;

namespace HMS.Data.Repositories.Implementations
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(AppDbContext context) : base(context)
        {
        }
    }
}
