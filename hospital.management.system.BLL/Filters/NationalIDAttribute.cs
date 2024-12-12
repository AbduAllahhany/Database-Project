using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class NationalIDAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string input = value.ToString();
            // Regular expression to validate a 14-digit number
            var nationalIdPattern = @"^\d{14}$";

            if (!Regex.IsMatch(input, nationalIdPattern))
            {
                return new ValidationResult("Invalid National ID. It must be a 14-digit number.");
            }
        }

        return ValidationResult.Success;
    }
}
