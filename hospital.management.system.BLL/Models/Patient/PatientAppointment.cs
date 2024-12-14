using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.BLL.Models.Patient;

public class PatientAppointment
{
   
    public Guid Id { get; set; }
    
   
    public string Status { get; set; } = null!;

   
    public string? Reason { get; set; }
    
    public string PatientName { get; set; } 
    
    public string DoctorName { get; set; } 
    
    public DateOnly Date { get; set; }
}