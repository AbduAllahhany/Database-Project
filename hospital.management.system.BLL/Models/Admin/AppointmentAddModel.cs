using System.ComponentModel.DataAnnotations;
using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class AppointmentAddModel
{
    public Guid? DoctorUserId { get; set; }
    public Guid? PatientUserId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string Reason { get; set; }
}