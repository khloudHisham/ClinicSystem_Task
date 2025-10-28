using clinicSystem.Models.Helper;

namespace clinicSystem.Services.AppointmentService
{
    public interface IAppointmentService
    {
        Task<GeneralResponse> ValidateAppointmentAsync(int doctorId, DateTime appointmentDateTime, TimeSpan appointmentTime, int Duration);

        // إضافة الدالة هنا
        Task<List<string>> GetFreeSlotsAsync(int doctorId, DateTime appointmentDate);

    }
}
