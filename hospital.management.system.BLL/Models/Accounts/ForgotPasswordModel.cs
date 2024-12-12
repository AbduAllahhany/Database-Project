
using System.ComponentModel.DataAnnotations;


namespace hospital.management.system.BLL.Models.Accounts;

    public class ForgotPasswordModel
    {
        [EmailAddress]
        public string Email { get; set; }
      

    }
    public class ForgetPasswordResponseModel
    {


    }

