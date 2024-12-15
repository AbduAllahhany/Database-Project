using System.ComponentModel.DataAnnotations;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace hospital.management.system.BLL.Filters
{
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userManager = (UserManager<ApplicationUser>)validationContext.GetService(typeof(UserManager<ApplicationUser>));
            var username = value as string;

            // Check for duplicate username in the database
            var existingUser = userManager.Users.FirstOrDefault(u => u.UserName == username);

            if (existingUser != null)
            {
                return new ValidationResult("Username is already in use.");
            }

            return ValidationResult.Success;
        }
    }
}