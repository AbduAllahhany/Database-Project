using System.ComponentModel.DataAnnotations;

namespace hospital.management.system.BLL.Models.Doctors;

public class FollowUpAppointmentModel
{
   // [Required]
    public Guid DoctorId { get; set; }
   // [Required]
    public Guid PatientId { get; set; }
    
    public string Status { get; set; } = "Approved";
    public DateOnly AppointmentDate { get; set; }
    public string? Reason { get; set; }
}