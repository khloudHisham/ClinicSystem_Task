using Microsoft.AspNetCore.Mvc;

namespace clinicSystem.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
