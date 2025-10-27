using clinicSystem.Models.Helper;

namespace clinicSystem.Services.AppointmentService
{
    public interface IAppointmentService
    {
        Task<GeneralResponse> ValidateAppointmentAsync(int doctorId, DateTime appointmentDateTime, TimeSpan appointmentTime, int Duration);

    }
}
