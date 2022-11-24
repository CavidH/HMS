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
                
            
            var k = HttpContext.Items;
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

        // [HttpPost]
        // public async Task<IActionResult> SignIn(UserPostVM postVm)
        // {
        //     await Register(postVm);
        //     return RedirectToAction(nameof(Index));
        // }

        // [Authorize]
        // public IActionResult Salam()
        // {
        //     return Content("salam");
        // }

        // [NonAction]
        // public async Task<IActionResult> Register(UserPostVM userPostVM)
        // {
        //     try
        //     {
        //         _unitOfWorkService.userService.Create(userPostVM);
        //     }
        //     catch (RegisterExceptions ex)
        //     {
        //         Console.WriteLine(ex);
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine(ex);
        //     }
        //
        //     return RedirectToAction(  "Index","Home");
        //
        //
        // }
    }
}

// public class UserPostVM
// {
//     public string Name { get; set; }
//     public string LastName { get; set; }
//     public string UserName { get; set; }
//     public string Email { get; set; }
//     public string Password { get; set; }
//     public string ConfirmPassword { get; set; }
// }
// }