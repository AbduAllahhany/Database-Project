using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace hospital.management.system.BLL.Models.Accounts;

public class LoginModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    [PasswordPropertyText] 
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}

public class LoginResponseModel
{
    public string UserName { get; set; }
    public bool RequiresTwoFactor { get; set; }
    public bool IsLockedOut { get; set; }
    public bool Succeeded { get; set; }
    public string Email { get; set; }

}

