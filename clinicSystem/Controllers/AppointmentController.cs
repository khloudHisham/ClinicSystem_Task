using Microsoft.AspNetCore.Mvc;

namespace clinicSystem.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
