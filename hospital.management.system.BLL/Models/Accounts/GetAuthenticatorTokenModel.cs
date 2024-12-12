
namespace hospital.management.system.BLL.Models.Accounts;

    public class GetAuthenticatorTokenModel
    {
        public string UserName { get; set; }


        
    }
    public class GetAuthenticatorKeyResponseModel
    {
        public string? Token { get; set; }
        public string? QRCodeUrl { get; set; }
        
    }

