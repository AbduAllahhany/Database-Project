namespace hospital.management.system.BLL.Models.Admin;

public class BillCreateModel
{
    public Guid PatientId { get; set; }

    public decimal PaidAmount { get; set; }

    public decimal TotalAmount { get; set; }
    
    public DateTime Date { get; set; }


}