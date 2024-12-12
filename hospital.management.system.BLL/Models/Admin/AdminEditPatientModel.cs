using hospital.management.system.BLL.Filters;

namespace hospital.management.system.BLL.Models.Admin;

public class AdminEditPatientModel
{
    public Guid? PatientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string SSN { get; set; }

    [DateOfBirth] public DateOnly DateOfBirth { get; set; }
    
    [Username]
    public string UserName { get; set; }
    

    public string Address { get; set; }
}