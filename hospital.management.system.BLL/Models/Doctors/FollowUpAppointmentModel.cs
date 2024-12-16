using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Models.Admin;

namespace hospital.management.system.BLL.Models.Doctors;

public class FollowUpAppointmentModel
{
   // [Required]
    public Guid DoctorId { get; set; }
   // [Required]
    public Guid PatientId { get; set; }
    
    public string Status { get; set; } = "Approved";
    public DateOnly AppointmentDate { get; set; }  = DateOnly.FromDateTime(DateTime.Now);
    public TimeOnly AppointmentTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
    public string? Reason { get; set; }
    public IEnumerable<UsernameIdModel>? PatientUsernameId { get; set; }
}