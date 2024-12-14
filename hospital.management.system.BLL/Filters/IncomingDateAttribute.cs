
using System.ComponentModel.DataAnnotations;


namespace hospital.management.system.BLL.Filters;


public class IncomingDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var dateOnly = (DateOnly)value;
        var utcNowDateOnly = DateOnly.FromDateTime(DateTime.UtcNow);

        return dateOnly > utcNowDateOnly ? true : false;
    }
}

