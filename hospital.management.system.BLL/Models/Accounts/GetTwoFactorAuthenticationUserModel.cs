

namespace hospital.management.system.BLL.Models.Accounts;

public class GetTwoFactorAuthenticationUserModel
{
    public bool rememberMe { set; get; }
    public string? returnUrl { set; get; }
}

