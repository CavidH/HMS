using System.Security.Claims;
using HMS.Business.Exceptions;
using HMS.Business.Services.Interfaces;
using HMS.Business.ViewModels;
using HMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public AccountController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User?.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Detail()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser user = await _unitOfWorkService.UserService.GetUserByIdAsync(userId);
            return View(user);
        }

        public IActionResult AccessDenied() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserRegisterVM userRegisterVm)
        {
            if (!ModelState.IsValid) return View(userRegisterVm);


            try
            {
                await _unitOfWorkService.UserService.CreateAsync(userRegisterVm);
            }
            catch (RegisterException ex)
            {
                ModelState.AddModelError("All", ex.Message);
                return View(userRegisterVm);
            }
            catch (Exception ex)
            {
                Log.Error($"Register Problem Email:{userRegisterVm.Email}," +
                          $"userIp:{HttpContext.Connection.RemoteIpAddress?.ToString()} ," +
                          $" Exception Detail :{ex.Message}");
                return View("/Views/Error/ErrorPage.cshtml");            }

            return RedirectToAction("Index", "DashBoard");
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginVM userLoginVm)
        {
            if (!ModelState.IsValid)
                return View(userLoginVm);


            try
            {
                await _unitOfWorkService.UserService.LoginAsync(userLoginVm);
            }
            catch (EmailNotFoundException ex)
            {
                ModelState.AddModelError("Email", ex.Message);
                return View(userLoginVm);
            }
            catch (LoginException ex)
            {
                ModelState.AddModelError("Password", ex.Message);
                return View(userLoginVm);
            }
            catch (Exception ex)
            {
                Log.Error($"Login Problem  user inputs Email:{userLoginVm.Email}," +
                          $" passvord: {userLoginVm.Password} " +
                          $"userIp:{HttpContext.Connection.RemoteIpAddress?.ToString()} ," +
                          $" Exception Detail :{ex.Message}");
                return View("/Views/Error/ErrorPage.cshtml");
              
            }

            return RedirectToAction("Index", "DashBoard");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _unitOfWorkService.UserService.LogOutAsync();
            return RedirectToAction("Index", "Home");
        }


        // public async Task<IActionResult> Roll()
        // {
        //     await _unitOfWorkService.UserService.CreateRollAsync();
        //     return Content("Okeydir");
        // }
    }
}