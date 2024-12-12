using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using hospital.management.system.DAL;

namespace hospital.management.system.BLL.Filters;

public class DoctorSpecializationAttribute : ValidationAttribute
{
    private static readonly IEnumerable<string> ValidSpecializations = SD.ValidSpecializations;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Specialization is required.");
        }

        string specialization = value.ToString();

        if (!ValidSpecializations.Contains(specialization))
        {
            return new ValidationResult($"Invalid specialization. Allowed values are: {string.Join(", ", ValidSpecializations)}.");
        }

        return ValidationResult.Success;
    }
}