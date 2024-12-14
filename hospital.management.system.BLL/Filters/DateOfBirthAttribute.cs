using System.ComponentModel.DataAnnotations;

namespace hospital.management.system.BLL.Filters;

public class DateOfBirthAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null) return false;
        var dateOfBirth = (DateOnly)value;
        DateTime minDate = DateTime.MinValue.Date; // Date part of DateTime.MinValue
        DateTime currentDate = DateTime.UtcNow.Date; // Current UTC date without time

        return dateOfBirth.ToDateTime(TimeOnly.MinValue) >= minDate &&
               dateOfBirth.ToDateTime(TimeOnly.MinValue) <= currentDate;

    }
}

