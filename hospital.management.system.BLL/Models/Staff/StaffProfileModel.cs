namespace hospital.management.system.BLL.Models.Staff;

public class StaffProfileModel : ProfileModel
{
    public Guid? UserId { get; set; }
    public string ? Name { get; set; }
    public string ? Role { get; set; }
}