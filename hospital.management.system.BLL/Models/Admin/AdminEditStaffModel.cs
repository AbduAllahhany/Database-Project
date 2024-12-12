namespace hospital.management.system.BLL.Models.Admin;

public class AdminEditStaffModel
{
    public string SSN;
    public Guid StaffId { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Address { get; set; }
}