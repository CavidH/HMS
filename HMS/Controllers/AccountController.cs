using HMS.Business.Exceptions;
using HMS.Business.Services.Interfaces;
using HMS.Business.ViewModels;
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
                Log.Error(ex.Message);
            }

            return RedirectToAction("Index", "Home");
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
            {
                return View(userLoginVm);
            }

            try
            {
                await _unitOfWorkService.UserService.LoginAsync(userLoginVm);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Index", "DashBoard");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _unitOfWorkService.UserService.LogOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Roll()
        {
            await _unitOfWorkService.UserService.CreateRollAsync();
            return Content("Okeydir");
        }
    }
}