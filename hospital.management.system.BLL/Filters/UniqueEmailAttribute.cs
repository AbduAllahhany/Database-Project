using System.ComponentModel.DataAnnotations;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace hospital.management.system.BLL.Filters
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        public string? UserIdPropertyName { get; }

        public UniqueEmailAttribute(string userIdPropertyName ="UserId")
        {
            UserIdPropertyName = userIdPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userManager =
                (UserManager<ApplicationUser>)validationContext.GetService(typeof(UserManager<ApplicationUser>));
            if (userManager == null)
            {
                throw new InvalidOperationException("UserManager is not available in the validation context.");
            }

            var email = value as string;
            if (string.IsNullOrEmpty(email))
            {
                return new ValidationResult("Email is required.");
            }

            // Get the current user's ID from the specified property
            var userIdProperty = validationContext.ObjectType.GetProperty(UserIdPropertyName);
            Guid userId = Guid.Empty;
            if (userIdProperty != null)
            {
                var temp = userIdProperty.GetValue(validationContext.ObjectInstance, null) as Guid?;
                userId = temp ?? Guid.Empty;
            }


            // Check if the email is already in use by another user
            var existingUser = userManager.FindByEmailAsync(email).Result;
            if (existingUser != null && existingUser.Id != userId)
            {
                return new ValidationResult("Email is already in use.");
            }

            return ValidationResult.Success;
        }
    }
}