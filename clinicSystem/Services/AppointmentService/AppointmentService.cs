using clinicSystem.Models.Data;
using clinicSystem.Models.Helper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace clinicSystem.Services.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> ValidateAppointmentAsync(int doctorId, DateTime appointmentDateTime, TimeSpan appointmentTime, int duration)
        {
            var dayOfWeek = appointmentDateTime.DayOfWeek.ToString();

            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(s => s.DoctorId == doctorId && s.DayOfWeek == dayOfWeek);

            if (schedule == null)
                return new GeneralResponse
                {
                    Status = false,
                    Message = "No schedule found for the selected doctor on this day."
                };

            if (appointmentTime < schedule.StartTime || appointmentTime > schedule.EndTime)
                return new GeneralResponse
                {
                    Status = false,
                    Message = "The appointment time is outside of working hours."
                };

            var newAppointmentEndTime = appointmentTime + TimeSpan.FromMinutes(duration);

            var appointments = await _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.AppointmentDate == appointmentDateTime.Date)
                .ToListAsync();

            bool conflict = appointments.Any(a =>
                appointmentTime < a.AppointmentTime.Add(TimeSpan.FromMinutes(a.Duration)) &&
                newAppointmentEndTime > a.AppointmentTime
            );

            if (conflict)
                return new GeneralResponse
                {
                    Status = false,
                    Message = "The selected appointment time is already booked."
                };

            return new GeneralResponse { Status = true };
        }

        public async Task<List<string>> GetFreeSlotsAsync(int doctorId, DateTime appointmentDate)
        {
            var schedule = await _context.Schedules.FirstOrDefaultAsync(s =>
                s.DoctorId == doctorId && s.DayOfWeek == appointmentDate.DayOfWeek.ToString());

            if (schedule == null) return new List<string>();

            var appointments = await _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.AppointmentDate == appointmentDate.Date)
                .ToListAsync();

            var slots = new List<string>();
            var currentTime = schedule.StartTime;

            while (currentTime + TimeSpan.FromMinutes(30) <= schedule.EndTime)
            {
                bool conflict = appointments.Any(a =>
                    currentTime < a.AppointmentTime.Add(TimeSpan.FromMinutes(a.Duration)) &&
                    currentTime + TimeSpan.FromMinutes(30) > a.AppointmentTime
                );


                if (!conflict) slots.Add(currentTime.ToString(@"hh\:mm"));



                currentTime += TimeSpan.FromMinutes(30);
            }

            return slots;
        }
    }
}
