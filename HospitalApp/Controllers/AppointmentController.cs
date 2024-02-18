using HospitalApp.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllViewModel();
            return View(list);
        }
    }
}

