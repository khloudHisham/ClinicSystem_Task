using clinicSystem.Models.Data;
using clinicSystem.Models.Entities;
using clinicSystem.Repositories;
using clinicSystem.Services.AppointmentService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace clinicSystem.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IGenericRepository<Appointment> _repository;
        private readonly ApplicationDbContext _context;
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IGenericRepository<Appointment> repository, ApplicationDbContext context, IAppointmentService appointmentService)
        {
            _repository = repository;
            _context = context;
            _appointmentService = appointmentService;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await _repository.GetAllAsync();
            return View(appointments);
        }

        public IActionResult New()
        {
            var appointment = new Appointment
            {
                Duration = 30 // Default visit length
            };
            FillDropdowns();
            return View("Create", appointment);
        }

        [HttpPost]
        public async Task<IActionResult> SaveNew(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var response = await _appointmentService.ValidateAppointmentAsync(
                    appointment.DoctorId,
                    appointment.AppointmentDate.Add(appointment.AppointmentTime),
                    appointment.AppointmentTime,
                    appointment.Duration
                );

                if (response.Status)
                {
                    await _repository.AddAsync(appointment);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["PopupData"] = response.Message;
                }
            }

            FillDropdowns(appointment.PatientId, appointment.DoctorId);
            return View("Create", appointment);
        }

        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<JsonResult> GetFreeSlots(int doctorId, DateTime appointmentDate)
        {
            var freeSlots = await _appointmentService.GetFreeSlotsAsync(doctorId, appointmentDate);
            return Json(freeSlots);
        }

        private void FillDropdowns(int? selectedPatientId = null, int? selectedDoctorId = null)
        {
            ViewBag.Patients = new SelectList(_context.Patients.ToList(), "Id", "Name", selectedPatientId);
            ViewBag.Doctors = new SelectList(_context.Doctors.ToList(), "Id", "Name", selectedDoctorId);
        }
    }
}
