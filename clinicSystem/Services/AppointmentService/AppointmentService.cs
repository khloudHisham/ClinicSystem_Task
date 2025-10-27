using clinicSystem.Models.Data;
using clinicSystem.Models.Helper;
using Microsoft.EntityFrameworkCore;

namespace clinicSystem.Services.AppointmentService
{
    public class AppointmentService: IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GeneralResponse> ValidateAppointmentAsync(int doctorId, DateTime appointmentDateTime, TimeSpan appoinmetTime, int Duration)
        {
            var dayOfWeek = appointmentDateTime.DayOfWeek.ToString();

            // 1. Verify that the Selected Doctor work on that Day
            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(s => s.DoctorId == doctorId && s.DayOfWeek == dayOfWeek);

            if (schedule == null)
                return new GeneralResponse
                {
                    Status = false,
                    Message = "No schedule found for the selected doctor on this day."
                };

            // 2. Verify that the AppointmentDate is in valid work Time for the Doctor

            // Convert appointment time to 12-hour format if necessary
            TimeSpan adjustedAppointmentTime = appoinmetTime.Hours > 12
                ? appoinmetTime.Subtract(TimeSpan.FromHours(12))
                : appoinmetTime;

            if (adjustedAppointmentTime < schedule.StartTime || adjustedAppointmentTime > schedule.EndTime)
                return new GeneralResponse
                {
                    Status = false,
                    Message = "The appointment time is outside of working hours."
                };

            // 3. Verify that the AppointmentDate not conflict with other appointments
            var newAppointmentEndTime = appoinmetTime + TimeSpan.FromMinutes(Duration);


            var appointments = await _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.AppointmentDate == appointmentDateTime.Date)
                .ToListAsync();


            bool conflict = appointments.Any(a =>
                appoinmetTime < a.AppointmentTime.Add(TimeSpan.FromMinutes(a.Duration)) &&
                newAppointmentEndTime > a.AppointmentTime
            );


            if (conflict)
                return new GeneralResponse
                {
                    Status = false,
                    Message = "The selected appointment time is already booked."
                };

            return new GeneralResponse
            {
                Status = true,
            };
        }
    }
}
