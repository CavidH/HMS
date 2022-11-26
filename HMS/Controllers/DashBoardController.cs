using System.Security.Claims;
using HMS.Business.Exceptions;
using HMS.Business.Services.Interfaces;
using HMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HMS.Controllers
{
    [Authorize]
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
            catch (UserNotFoundException ex)
            {
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Log.Error($"User ID{userId}," +
                          $"userIp:{HttpContext.Connection.RemoteIpAddress?.ToString()} ," +
                          $" Exception Detail :{ex.Message}");
                return View("/Views/Error/ErrorPage.cshtml");


            }

            return View(user);
        }
    }
}