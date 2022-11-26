using System.Security.Claims;
using HMS.Business.Exceptions;
using HMS.Business.Services.Interfaces;
using HMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HMS.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public DoctorController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        public async Task<IActionResult> AllDoc()
        {
            List<Doctor> doctors = await _unitOfWorkService.DoctorService.GetAllAsync();
            return View(doctors);
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0) return NotFound();
            Doctor doctor;
            try
            {
                doctor = await _unitOfWorkService.DoctorService.GetByIdAsync(id);
            }
            catch (UserNotFoundException ex)
            {
              
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Log.Error($"User Detail Error ID ," +
                          $"userIp:{HttpContext.Connection.RemoteIpAddress?.ToString()} ," +
                          $" Exception Detail :{ex.Message}");
                return View("/Views/Error/ErrorPage.cshtml");
            }
            return View(doctor);
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
            catch (Exception ex)
            {
                Log.Error($"Add doctor Error   ," +
                          $"userIp:{HttpContext.Connection.RemoteIpAddress?.ToString()} ," +
                          $" Exception Detail :{ex.Message}");
                return View("/Views/Error/ErrorPage.cshtml");
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
                await _unitOfWorkService.DoctorService.RemoveAsync(userId, id);
            }
            catch (Exception ex)
            {
                Log.Error($"RemoveDoctor doctor Error   ," +
                          $"userIp:{HttpContext.Connection.RemoteIpAddress?.ToString()} ," +
                          $" Exception Detail :{ex.Message}");
                return View("/Views/Error/ErrorPage.cshtml");
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

        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DocPatients()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Patient> patients = await _unitOfWorkService
                .PatientService
                .GetDoctorPatientsAsync(userId);
            return View(patients);
        }
    }
}