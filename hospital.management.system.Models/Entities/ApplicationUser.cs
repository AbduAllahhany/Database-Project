using hospital.management.system.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace hospital.management.system.Models.Entities;

public class ApplicationUser: IdentityUser<Guid>
{
    public string SSN { get; set; }
    public Gender Gender { get; set; }
}