using System.ComponentModel.DataAnnotations;

namespace hospital.management.system.BLL.Models.Admin;

public class BillCreateModel
{
    [Required] public Guid? PatientId { get; set; }

    [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "Enter a valid number")]
    [Required]
    public decimal PaidAmount { get; set; }

    [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "Enter a valid number")]
    [Required]
    public decimal TotalAmount { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;


}