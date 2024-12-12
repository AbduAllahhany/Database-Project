using System.Runtime.InteropServices;
using hospital.management.system.BLL.Filters;
using hospital.management.system.DAL;
using hospital.management.system.Models.Enums;

namespace hospital.management.system.BLL.Models.Admin;

public class StaffCreateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    [ValidRole(SD.Nurse, SD.Intern)]
    public string Role { get; set; }
    public Guid DeptId { get; set; }
    public TimeOnly StartSchedule { get; set; }
    public TimeOnly EndSchedule { get; set; }
    public byte DayOfWork { get; set; }
    public string UserName { get; set; }
    public Gender Gender { get; set; }
    
    public string SSN { get; set; }
}