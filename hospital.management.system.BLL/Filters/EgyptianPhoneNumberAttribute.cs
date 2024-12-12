using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace hospital.management.system.BLL.Filters;

public class EgyptianPhoneNumberAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Phone number is required.");
        }

        string phoneNumber = value.ToString();

        // Regular expression to match Egyptian phone numbers
        string pattern = "^01[0125][0-9]{8}$";

        if (!Regex.IsMatch(phoneNumber, pattern))
        {
            return new ValidationResult("Invalid Egyptian phone number. It should start with '01' followed by 2, 0, 1, or 5, and 8 digits.");
        }

        return ValidationResult.Success;
    }
}