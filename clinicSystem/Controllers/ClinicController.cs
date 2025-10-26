using Microsoft.AspNetCore.Mvc;

namespace clinicSystem.Controllers
{
    public class ClinicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
