namespace hospital.management.system.Models.Entities;

public class BaseEntity
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
}