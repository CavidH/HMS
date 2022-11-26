using HMS.Core.Abstracts;
using HMS.Core.Entities;
using HMS.Data.DAL;
using Microsoft.EntityFrameworkCore;

namespace HMS.Data.Repositories.Implementations
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetDoctorPatientsAsync(string docId)
        {
            IEnumerable<Patient> patients = await
                _context
                    .Doctor
                    .Where(p => p.AppUserId == docId)
                    .Include(p => p.DoctorPatients)
                    .ThenInclude(p => p.Patient)
                    .ThenInclude(p=>p.AppUser)
                    .Select(p => p.DoctorPatients
                        .Where(p => p.IsDeleted == false)
                        .Select(p => p.Patient))
                    .FirstOrDefaultAsync();


            return patients;
        }
    }
}