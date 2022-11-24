using HMS.Business.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using HMS.Core.Entities;

namespace HMS.Business.Services.Implementations
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IUserService _userService;


        public UnitOfWorkService(
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
             
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        // private IHeadSlideService headSlideService { get; }


        

        public IUserService userService => _userService =
            _userService = _userService ?? new UserService(_userManager, _signInManager, _roleManager);

       
    }
}
