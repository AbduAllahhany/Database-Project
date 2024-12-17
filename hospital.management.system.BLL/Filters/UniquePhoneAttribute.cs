using System.ComponentModel.DataAnnotations;
using System.Linq;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace hospital.management.system.BLL.Filters
{
    public class UniquePhoneNumberAttribute : ValidationAttribute
    {
        public string UserIdPropertyName { get; }

        public UniquePhoneNumberAttribute(string userIdPropertyName="")
        {
            UserIdPropertyName = userIdPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get UserManager from the validation context
            var userManager = (UserManager<ApplicationUser>)validationContext.GetService(typeof(UserManager<ApplicationUser>));
            if (userManager == null)
            {
                throw new InvalidOperationException("UserManager is not available in the validation context.");
            }

            var phoneNumber = value as string;
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return ValidationResult.Success; // Assume null or empty phone numbers are valid
            }

            // Get the user ID from the specified property
            var userIdProperty = validationContext.ObjectType.GetProperty(UserIdPropertyName);
            Guid userId = Guid.Empty;
            if (userIdProperty != null)
            {
                var temp = userIdProperty.GetValue(validationContext.ObjectInstance, null) as Guid?;
                userId = temp ?? Guid.Empty;
            }
            // Check if the phone number already exists and belongs to a different user
            var existingUser = userManager.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
            if (existingUser != null && existingUser.Id != userId)
            {
                return new ValidationResult("Phone number is already in use.");
            }

            return ValidationResult.Success;
        }
    }
}
