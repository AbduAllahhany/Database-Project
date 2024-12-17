using hospital.management.system.BLL.Filters;

namespace hospital.management.system.BLL.Models.Admin;

public class EmergencyContactCreateModel
{
    public string Name { get; set; }
    [EgyptianPhoneNumber] public string Phone { get; set; }
    public string RelationShip { get; set; }
    public Guid? PatientId { get; set; }
}