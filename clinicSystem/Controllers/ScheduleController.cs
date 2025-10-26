using Microsoft.AspNetCore.Mvc;

namespace clinicSystem.Controllers
{
    public class ScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
