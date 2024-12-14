using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;

namespace hospital.management.system.BLL.Models.Admin;

public class InsuranceCreateModel
{
    [Required] 
    [Display(Name = "Provide Name")]
    public string? ProviderName { get; set; }
    [Display(Name = "Policy Number")]
    [Required] public string? PolicyNumber { get; set; }
    [Display(Name = "Coverage Money")]
    [Required] public decimal CoverageMoney { get; set; }
    [Display(Name = "Expiry Date")]
    [Required] [IncomingDate] public DateOnly ExpiryDate { get; set; }
    [Required] public Guid? PatientId { get; set; }
}