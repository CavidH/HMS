using System.Text;
using HMS.Business.Exceptions;
using HMS.Business.Services.Interfaces;
using HMS.Business.ViewModels;
using HMS.Core.Abstracts;
using HMS.Core.Entities;
using HMS.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace HMS.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _untiOfWork;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork untiOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _untiOfWork = untiOfWork;
        }

        public async Task CreateAsync(UserRegisterVM userRegisterVm)
        {
            BloodGroup bloodGroup;
            Gender gender;
            UserRoles role;

            if (!Enum.TryParse(userRegisterVm.BloodGroup, out bloodGroup))
                throw new RegisterException("Blood Type Exception");
            if (!Enum.TryParse(userRegisterVm.Gender, out gender))
                throw new RegisterException("Gender Type Exception");
            // if (!Enum.TryParse(userRegisterVm.Role, out role))
            //     throw new RegisterException("Role Type Exception");


            AppUser newUser = new AppUser
            {
                FirstName = userRegisterVm.FirstName,
                LastName = userRegisterVm.LastName,
                UserName = userRegisterVm.UserName,
                Email = userRegisterVm.Email,
                IsActivated = true,
                CreatedAt = DateTime.Now,
                Gender = gender,
                BloodGroup = bloodGroup,
                PhoneNumber = userRegisterVm.PhoneNumber,
            };

            IdentityResult identityResult = await _userManager.CreateAsync(newUser, userRegisterVm.Password);
            if (!identityResult.Succeeded)
            {
                StringBuilder errorBuilder = new StringBuilder();
                foreach (var error in identityResult.Errors)
                {
                    errorBuilder.Append(error.Description);
                    // errors.Add(error.Description);
                    // ModelState.AddModelError("", error.Description);
                    //return Json(error.Description);
                }

                throw new RegisterException(errorBuilder.ToString());
            }

            await _userManager.AddToRoleAsync(newUser, userRegisterVm.Role);
            /*   await _userManager.AddToRoleAsync(newUser, UserRoles.Admin.ToString());
                For Admin Register
                
                */
            if (userRegisterVm.Role == UserRoles.Doctor.ToString())
            {
                await _untiOfWork.DoctorRepository.CreateAsync(new Doctor { AppUserId = newUser.Id });
                await _untiOfWork.SaveAsync();
            }
            else if (userRegisterVm.Role == UserRoles.Patient.ToString())
            {
                await _untiOfWork.PatientRepository.CreateAsync(new Patient { AppUserId = newUser.Id });
                await _untiOfWork.SaveAsync();
            }


            await _signInManager.SignInAsync(newUser, isPersistent: userRegisterVm.StayLoggedIn);
        }

        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user is null)
                throw new UserNotFoundExceptionException(id + " - Not Found ");

            return user;
        }

        public async Task CreateRollAsync()
        {
            foreach (var UserRole in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(UserRole.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = UserRole.ToString() });
                }
            }

            Console.WriteLine("role done");
        }


        public async Task LoginAsync(UserLoginVM userLoginVm)
        {
            AppUser user = await _userManager.FindByEmailAsync(userLoginVm.Email);
            if (user is null) throw new EmailNotFoundException(userLoginVm.Email + " Not Found ");

            SignInResult signResult =
                await _signInManager
                    .PasswordSignInAsync(user, userLoginVm.Password, userLoginVm.RememberMe, false);
            if (!signResult.Succeeded)
                throw new LoginException(signResult.ToString());
        }

        public async Task LogOutAsync() => await _signInManager.SignOutAsync();
    }
}