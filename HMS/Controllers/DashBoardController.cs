using System.Security.Claims;
using HMS.Business.Exceptions;
using HMS.Business.Services.Interfaces;
using HMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    [Authorize /*(Roles = "Patient")*/]
    public class DashBoardController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public DashBoardController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser user = null;
            try
            {
                user = await _unitOfWorkService.UserService.GetUserByIdAsync(userId);
            }
            catch (UserNotFoundExceptionException ex)
            {
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                //todo log
            }

            return View(user);
        }
    }
}