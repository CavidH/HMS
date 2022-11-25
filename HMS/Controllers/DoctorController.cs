using System.Security.Claims;
using HMS.Business.Exceptions;
using HMS.Business.Services.Interfaces;
using HMS.Core.Entities;
using HMS.Core.Enums;
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

        [Authorize]
        public async Task<IActionResult> Index( /*int page = 1*/)
        {
            return View(await _unitOfWorkService.DoctorService.GetAllAsync());
        }


        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> AddDoctor(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _unitOfWorkService.DoctorPatientService.CreateAsync(userId, id);
                // Already Available
            }
            catch (DoctorPatientException ex)
            {
                TempData["AlertErr"] = "Already Available";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                //todo log
                Console.WriteLine(e);
                throw;
            }

            TempData["Alert"] = "Added";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> RemoveDoctor(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _unitOfWorkService.DoctorPatientService.RemoveAsync(userId, id);
            }
            catch (Exception e)
            {
                //todo log
                Console.WriteLine(e);
                throw;
            }

            TempData["AlertErr"] = "Deleted";
            return RedirectToAction(nameof(RegDoctors));
        }

        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> RegDoctors()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Doctor> doctors = await _unitOfWorkService.DoctorService.GetPatientDoctorsAsync(userId);
            return View(doctors);
        }
    }
}