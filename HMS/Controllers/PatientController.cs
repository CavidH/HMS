using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HMS.Business.Services.Interfaces;

namespace HMS.Controllers
{
    public class PatientController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public PatientController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        // [Authorize(Roles = "Doctor")]
        // public async Task<IActionResult> Index()
        // {
        //     string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //     IEnumerable<Patient> patients = await _unitOfWorkService
        //         .PatientService
        //         .GetDoctorPatientsAsync(userId);
        //     return View(patients);
        // }

        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Remove(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _unitOfWorkService.PatientService.RemoveAsync(userId, id);
            return RedirectToAction("DocPatients", "Doctor");
        }
    }
}