namespace hospital.management.system.BLL.Models.Admin;

public class InsuranceCreateModel
{
    public string ProviderName { get; set; }
    public string PolicyNumber { get; set; }
    public decimal CoverageMoney { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public Guid PatientId { get; set; }
}