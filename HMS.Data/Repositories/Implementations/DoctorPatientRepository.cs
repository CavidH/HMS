using HMS.Core.Abstracts;
using HMS.Core.Entities;
using HMS.Data.DAL;
using Microsoft.EntityFrameworkCore;

namespace HMS.Data.Repositories.Implementations
{
    public class DoctorPatientRepository : Repository<DoctorPatient>, IDoctorPatientRepository
    {
        private readonly AppDbContext _context;

        public DoctorPatientRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsExistAsync(int patientId, int doctorId)
        {
           DoctorPatient? doctorPatient = await _context.DoctorPatient
                .Where(p => p.PatientId == patientId && p.DoctorId == doctorId&&p.IsDeleted==false)
                .FirstOrDefaultAsync();

            return doctorPatient != null ? true : false;
        }
    }
}