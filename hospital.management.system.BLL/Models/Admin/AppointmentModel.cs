using System.ComponentModel.DataAnnotations;
using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class AppointmentModel
{
    public Guid? DoctorUserId { get; set; }
    public Guid? PatientUserId { get; set; }

    [Display(Name = "Appointment Date")] public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    [Display(Name = "Appointment Time")] public TimeOnly Time { get; set; }
    [Required] public string? Reason { get; set; }
    
    public IEnumerable<UsernameIdModel>? PatientUsernameId { get; set; }
    public IEnumerable<UsernameIdModel>? DoctorUsernameId { get; set; }
}