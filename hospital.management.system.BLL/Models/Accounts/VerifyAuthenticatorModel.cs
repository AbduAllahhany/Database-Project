
using System.ComponentModel.DataAnnotations;


namespace hospital.management.system.BLL.Models.Accounts;

public class VerifyAuthenticatorModel
{

    [Required] public string Code { get; set; }
    public string? ReturnUrl { get; set; }
    [Display(Name = "Remember me?")] public bool RememberMe { get; set; }
}

public class VerifyAuthenticatorResponseModel
{
    public bool IsLockedOut { get; set; }
    public bool Succeeded { get; set; }
}

