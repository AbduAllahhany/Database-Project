using System.ComponentModel.DataAnnotations;
using System.Linq;
using hospital.management.system.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace hospital.management.system.BLL.Filters
{
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        public string UserIdPropertyName { get; }

        public UniqueUsernameAttribute(string userIdPropertyName="")
        {
            UserIdPropertyName = userIdPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            // Get UserManager from the DI container
            var userManager = (UserManager<ApplicationUser>)validationContext.GetService(typeof(UserManager<ApplicationUser>));
            if (userManager == null)
            {
                throw new InvalidOperationException("UserManager is not available in the validation context.");
            }

            var username = value as string;
            if (string.IsNullOrEmpty(username))
            {
                return ValidationResult.Success; // Consider null or empty usernames as valid
            }

            // Get the user ID from the specified property
            var userIdProperty = validationContext.ObjectType.GetProperty(UserIdPropertyName);
            Guid userId = Guid.Empty;
            if (userIdProperty != null)
            {
                var temp = userIdProperty.GetValue(validationContext.ObjectInstance, null) as Guid?;
                userId = temp ?? Guid.Empty;
            }

            // Check for existing user with the same username
            var existingUser = userManager.Users.FirstOrDefault(u => u.UserName == username);
            if (existingUser != null && existingUser.Id != userId)
            {
                // Username exists and belongs to a different user
                return new ValidationResult("Username is already in use.");
            }

            return ValidationResult.Success;
        }
    }
}
