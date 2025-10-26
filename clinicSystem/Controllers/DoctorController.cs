using clinicSystem.Models.Data;
using clinicSystem.Models.Entities;
using clinicSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace clinicSystem.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IGenericRepository<Doctor> _repository;
        private readonly ApplicationDbContext _context;
        public DoctorController(IGenericRepository<Doctor> repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var doctors = await _repository.GetAllAsync();
            return View(doctors);
        }

        public IActionResult New()
        {
            ViewBag.Clinics = new SelectList(_context.Clinics, "Id", "Name");
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> SaveNew(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(doctor);
                return RedirectToAction("Index");
            }

            ViewBag.Clinics = new SelectList(_context.Clinics, "Id", "Name");
            return View("Create", doctor);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
