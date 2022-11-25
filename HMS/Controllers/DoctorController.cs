using System.Security.Claims;
using HMS.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public DoctorController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        public async Task<IActionResult> Index( /*int page = 1*/)
        {
            return View(await _unitOfWorkService.DoctorService.GetAllAsync());
        }

        public async Task<IActionResult> AddDoctor(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _unitOfWorkService.DoctorPatientService.CreateAsync(userId, id);
            ViewBag.msg = "Added";
            return RedirectToAction(nameof(Index));
        }
    }
}