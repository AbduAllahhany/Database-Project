using System.Runtime.InteropServices.JavaScript;
using hospital.management.system.BLL.Filters;

namespace hospital.management.system.BLL.Models.Admin;

public class AdminDoctorEditModel
{
    [Username] public string UserName { get; set; }
    public Guid? UserId { get; set; }
    public Guid? DoctorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Email { get; set; }
    public string SSN { get; set; }

    public string PhoneNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Address { get; set; }
}