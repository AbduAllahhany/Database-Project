namespace hospital.management.system.BLL.Models.Admin;

public class GetAllAdminsResponseModel
{ 
    public Guid Id { get; set; } 
    public string  Email{ get; set; }
    public string  UserName{ get; set; }
    public string  PhoneNumber{ get; set; }
}