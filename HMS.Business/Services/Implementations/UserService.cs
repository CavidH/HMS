using System.Text;
using HMS.Business.Exceptions;
using HMS.Business.Services.Interfaces;
using HMS.Business.ViewModels;
using HMS.Core.Entities;
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
            AppUser newUser = new AppUser
            {
                Name = userRegisterVm.FirstName,
                LastName = userRegisterVm.LastName,
                UserName = userRegisterVm.UserName,
                Email = userRegisterVm.Email,
                IsActivated = true,
                CreatedAt = DateTime.Now
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

                throw new RegisterExceptions(errorBuilder.ToString());
            }

            // var Token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            // var ConfirmationLink = Url.Action("ConfirmEmail", "Account",
            // new { userId = newUser.Id, token = Token }, Request.Scheme);


            // EmailHelper.EmailContentBuilder(register.Email, ConfirmationLink, "Confirm Email");

            await _signInManager.SignInAsync(newUser, isPersistent: userRegisterVm.StayLoggedIn);

            // return RedirectToAction("Index", "Home");
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