using System.ComponentModel.DataAnnotations;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace hospital.management.system.BLL.Filters
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userManager = (UserManager<ApplicationUser>)validationContext.GetService(typeof(UserManager<ApplicationUser>));
            // var userId = (string)validationContext.ObjectType.GetProperty("Id").GetValue(validationContext.ObjectInstance, null);
            var email = value as string;

            var existingUser = userManager.FindByEmailAsync(email).Result;
            if (existingUser != null)
            {
                return new ValidationResult("Email is already in use.");
            }

            return ValidationResult.Success;
        }
    }

}