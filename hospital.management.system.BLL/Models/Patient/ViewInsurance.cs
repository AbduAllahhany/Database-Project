using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.BLL.Models.Patient;

public class ViewInsurance
{
    public Guid Id { get; set; }
    
    public string PatientName { get; set; }

   
    public DateOnly ExpiryDate { get; set; }
    
   
    public decimal? CoverageMoney { get; set; }
 
    public string PolicyNumber { get; set; } = null!;
    
    public string ProviderName { get; set; } = null!;




}