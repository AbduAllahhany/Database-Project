using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.BLL.Models.Patient;

public class PatientVisit
{
    public string PatientName { get; set; } 
    
   
    public DateOnly Date { get; set; }   
    
    public string? Notes { get; set; }
    
    public string Reason { get; set; } = null!;


}