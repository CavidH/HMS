using System.Text;
using HMS.Business.Exceptions;
using HMS.Business.Services.Interfaces;
using HMS.Business.ViewModels;
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

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
                    errorBuilder.Append(error);
                    // errors.Add(error.Description);
                    // ModelState.AddModelError("", error.Description);
                    //return Json(error.Description);
                }

                throw new RegisterException(errorBuilder.ToString());
            }

            await _userManager.AddToRoleAsync(newUser, userRegisterVm.Role);

            // var Token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            // var ConfirmationLink = Url.Action("ConfirmEmail", "Account",
            // new { userId = newUser.Id, token = Token }, Request.Scheme);


            // EmailHelper.EmailContentBuilder(register.Email, ConfirmationLink, "Confirm Email");

            await _signInManager.SignInAsync(newUser, isPersistent: userRegisterVm.StayLoggedIn);

            // return RedirectToAction("Index", "Home");
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


        public Task Login(UserLoginVM userLoginVm)
        {
            throw new NotImplementedException();
        }

        public Task LogOut()
        {
            throw new NotImplementedException();
        }
    }
}