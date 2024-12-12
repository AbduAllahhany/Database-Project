


using System.ComponentModel.DataAnnotations;

namespace hospital.management.system.BLL.Models.Accounts;


    public class ConfirmEmailModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Token { get; set; }  
    }

