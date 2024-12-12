using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.BLL.Models.Patient;

public class PatientRoom
{
    public string PatientName { get; set; }  
    
   
    public DateOnly StartDate { get; set; }

   
    public DateOnly EndDate { get; set; }
    
   
    public decimal CostPerDay { get; set; }

   
    public int RoomNumber { get; set; }

    public string? Type { get; set; }

   
    public bool Status { get; set; }
}