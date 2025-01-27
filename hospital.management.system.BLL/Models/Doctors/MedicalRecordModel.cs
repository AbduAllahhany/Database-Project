using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Models.Admin;

namespace hospital.management.system.BLL.Models.Doctors;

public class MedicalRecordModel
{
    public Guid LoggedDoctorId { get; set; }
    public Guid SelectedPatientId { get; set; }
    [Required] public string? Diagnostic { get; set; }

    [Required] public string? Prescription { get; set; }
    
    public IEnumerable<UsernameIdModel>? PatientUsernameId { get; set; }
    //[Required]
    //public DateOnly dateOfRecording { get; set; }
}