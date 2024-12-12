using System.ComponentModel.DataAnnotations.Schema;

namespace hospital.management.system.BLL.Models.Patient;

public class PatientBill
{
    public Guid Id { get; set; }
    
    public string PatientName { get; set; } 
   
    public decimal TotalAmount { get; set; }

  
    public decimal PaidAmount { get; set; }

  
    public decimal? Remaining { get; set; }
}