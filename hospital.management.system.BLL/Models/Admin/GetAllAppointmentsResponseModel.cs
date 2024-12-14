using System.ComponentModel.DataAnnotations;
using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class GetAllAppointmentsResponseModel
{

    public Guid Id { get; set; }
    
    [Required] public string? PatientUsername { get; set; }
    [Required] public string? DoctorUsername { get; set; }
    [Required] public string? Status { get; set; }
    [Required] public string? Reason { get; set; }
    [Required] public DateOnly? Date { get; set; }
    [Required] public TimeOnly? Time { get; set; }
}