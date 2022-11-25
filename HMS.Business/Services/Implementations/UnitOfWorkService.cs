using HMS.Business.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using HMS.Core.Entities;
using HMS.Data.Repositories;
using HMS.Core.Abstracts;

namespace HMS.Business.Services.Implementations
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private IUserService _userService;
        private IDoctorService _doctorService;
        private IDoctorPatientService _doctorPatientService;
        private IPatientService _patientService;


        public UnitOfWorkService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
        // private IHeadSlideService headSlideService { get; }


        public IUserService UserService => _userService ??=
            new UserService(_userManager, _signInManager, _roleManager, _unitOfWork);

        public IDoctorService DoctorService => _doctorService ??= new DoctorService(_unitOfWork);

        public IDoctorPatientService DoctorPatientService =>
            _doctorPatientService ??= new DoctorPatientService(_unitOfWork);

        public IPatientService PatientService => _patientService ??= new PatientService();
    }
}