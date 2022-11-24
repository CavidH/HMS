using HMS.Business.Exceptions;
using HMS.Business.Services.Interfaces;
using HMS.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserRegisterVM userRegisterVm)
        {
            if (!ModelState.IsValid) return View(userRegisterVm);


            try
            {
                await _unitOfWorkService.userService.CreateAsync(userRegisterVm);
            }
            catch (RegisterExceptions ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return RedirectToAction("Index", "Home");
        }





        // public async Task<IActionResult> Roll()
        // {
        //     await _unitOfWorkService.userService.CreateRollAsync();
        //     return Content("Okeydir");
        // }



    }
}