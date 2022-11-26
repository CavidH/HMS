namespace HMS.Business.Services.Interfaces
{
    public interface IDoctorPatientService
    {
        public Task CreateAsync(string userId ,int doctorId);
    }
}
