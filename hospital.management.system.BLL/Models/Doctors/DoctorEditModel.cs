using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;
using hospital.management.system.Models.Enums;
namespace hospital.management.system.BLL.Models.Doctors;

public class DoctorEditModel
{
    public Guid UserId { get; set; }


    [Username] [UniqueUsername("UserId")] [Display(Name ="Username")]public string UserName { get; set; }

    [Display(Name = "First Name")] public string FirstName { get; set; }

    [Display(Name = "Last Name")] public string LastName { get; set; }

    [Display(Name = "Phone Number")]
    [EgyptianPhoneNumber]
    [UniquePhoneNumber("UserId")]
    public string PhoneNumber { get; set; }

}