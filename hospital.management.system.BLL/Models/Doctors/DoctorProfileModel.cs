using System.ComponentModel.DataAnnotations;

namespace hospital.management.system.BLL.Models.Doctors;

public class DoctorProfileModel : ProfileModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    [Display(Name = "Department Name")]
    public string DepartmentName { get; set; }

    public string Specialization { get; set; }
}