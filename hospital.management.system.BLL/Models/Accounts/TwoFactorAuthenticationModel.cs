

namespace hospital.management.system.BLL.Models.Accounts;

public class TwoFactorAuthenticationModel
{
    public string UserName { get; set; }
    public string Code { get; set; }
    public string? Token { get; set; }
    public string? QRCodeUrl { get; set; }
}

public class TwoFactorAuthenticationResponseModel
{
    public string? Msg { get; set; }
}

