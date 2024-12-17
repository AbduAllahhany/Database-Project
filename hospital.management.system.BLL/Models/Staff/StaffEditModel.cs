using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;
using hospital.management.system.DAL;

namespace hospital.management.system.BLL.Models.Staff;

public class StaffEditModel
{
    public Guid Id { get; set; }
    [Display(Name = "First Name")] public string FirstName { get; set; }
    [Display(Name = "Last Name")] public string LastName { get; set; }
    [Display(Name = "Phone Number")] public string PhoneNumber { get; set; }
    public string Username { get; set; }
}