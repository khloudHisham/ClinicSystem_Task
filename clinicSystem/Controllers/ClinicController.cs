using clinicSystem.Models.Entities;
using clinicSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace clinicSystem.Controllers
{
    public class ClinicController : Controller
    {

        private readonly IGenericRepository<Clinic> _repository;
        public ClinicController(IGenericRepository<Clinic> repository)
        {
            _repository = repository;
        }
        // GET: Clinic
        public async Task<IActionResult> Index()
        {
            var clinics = await _repository.GetAllAsync();
            return View(clinics);
        }
        public IActionResult New()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> SaveNew(Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(clinic);
                return RedirectToAction("Index");
            }
            return View("Create", clinic);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
