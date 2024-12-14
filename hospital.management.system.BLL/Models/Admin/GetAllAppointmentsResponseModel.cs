using System.ComponentModel.DataAnnotations;
using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class GetAllAppointmentsResponseModel
{
    public string? PatientUsername { get; set; }
    public string? DoctorUsername { get; set; }
    [Required]
    public Status? Status { get; set; }
    [Required]
    public string? Reason { get; set; }
    [Required]
    public DateOnly? Date { get; set; }
    [Required]
    public TimeOnly? Time { get; set; }

}