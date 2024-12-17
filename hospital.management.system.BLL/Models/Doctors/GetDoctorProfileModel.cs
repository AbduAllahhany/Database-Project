using System.ComponentModel.DataAnnotations;

namespace hospital.management.system.BLL.Models.Doctors;

public class GetDoctorProfileModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Display(Name = "Start Schedule")] public TimeOnly StartSchedule { get; set; }
    [Display(Name = "End Schedule")] public TimeOnly EndSchedule { get; set; }
    [Display(Name = "Department Name")] public string DepartmentName { get; set; }
    
    public decimal Salary { get; set; }
    public string Specialization { get; set; }
}