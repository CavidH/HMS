using HMS.Core.Abstracts;
using HMS.Core.Entities;
using HMS.Data.DAL;
using Microsoft.EntityFrameworkCore;

namespace HMS.Data.Repositories.Implementations
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetPatientDoctorsAsync(string patientId)
        {
            IEnumerable<Doctor> doctors = await
                _context
                    .Patient
                    .Where(p => p.AppUserId == patientId)
                    .Include(p => p.PatientDoctor)
                    .ThenInclude(p => p.Doctor)
                    .ThenInclude(p => p.AppUser)
                    .Select(p => p.PatientDoctor
                        .Where(p => p.IsDeleted == false)
                        .Select(p => p.Doctor))
                    .FirstOrDefaultAsync();
            return doctors.ToList();
        }
    }
}