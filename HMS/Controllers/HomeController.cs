using HMS.Business.Services.Interfaces;
using HMS.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWork;

        public HomeController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            List<Doctor> doctors = await _unitOfWork.DoctorService.GetAllAsync();
            return View(doctors);
        }
    }
}