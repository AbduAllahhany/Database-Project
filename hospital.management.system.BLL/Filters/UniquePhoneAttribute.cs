using System.ComponentModel.DataAnnotations;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace hospital.management.system.BLL.Filters
{
    public class UniquePhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userManager = (UserManager<ApplicationUser>)validationContext.GetService(typeof(UserManager<ApplicationUser>));
            var phoneNumber = value as string;

            // Check if the phone number already exists in the database
            var existingUser = userManager.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);

            if (existingUser != null)
            {
                return new ValidationResult("Phone number is already in use.");
            }

            return ValidationResult.Success;
        }
    }
}
