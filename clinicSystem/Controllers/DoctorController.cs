using Microsoft.AspNetCore.Mvc;

namespace clinicSystem.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
