using System.ComponentModel.DataAnnotations;

namespace hospital.management.system.BLL.Models.Doctors;

public class MedicalRecordModel
{
    public Guid LoggedDoctorId { get; set; }
    public Guid SelectedPatientId { get; set; }
    [Required] public string? Diagnostic { get; set; }

    [Required] public string? Prescription { get; set; }
    //[Required]
    //public DateOnly dateOfRecording { get; set; }
}