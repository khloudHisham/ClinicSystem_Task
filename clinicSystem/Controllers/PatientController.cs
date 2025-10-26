using clinicSystem.Models.Entities;
using clinicSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace clinicSystem.Controllers
{
    public class PatientController : Controller
    {
        private readonly IGenericRepository<Patient> _repository;
        public PatientController(IGenericRepository<Patient> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var patients = await _repository.GetAllAsync();
            return View(patients);
        }
        public IActionResult New()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> SaveNew(Patient patient)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(patient);
                return RedirectToAction("Index");
            }
            return View("Create", patient);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
