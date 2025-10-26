using clinicSystem.Models.Data;
using clinicSystem.Models.Entities;
using clinicSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace clinicSystem.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IGenericRepository<Schedule> _repository;
        private readonly ApplicationDbContext _context;
        public ScheduleController(IGenericRepository<Schedule> repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var schedules = await _repository.GetAllAsync();
            return View(schedules);
        }

        public IActionResult New()
        {
            ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "Name");
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> SaveNew(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(schedule);
                return RedirectToAction("Index");
            }

            ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "Name");
            return View("Create", schedule);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
