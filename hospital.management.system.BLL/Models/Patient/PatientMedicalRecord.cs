using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.BLL.Models.Patient;

public class PatientMedicalRecord
{
    public string PatientName { get; set; } 
    
    public string DoctorName { get; set; } 

   
    public string? Prescription { get; set; }
    
  
    public string? Diagnostic { get; set; }

    
    public DateOnly DateOfRecording { get; set; }


}