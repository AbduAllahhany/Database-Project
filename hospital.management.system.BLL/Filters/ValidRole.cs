namespace hospital.management.system.BLL.Filters;

using System.ComponentModel.DataAnnotations;
using System.Linq;

public class ValidRoleAttribute : ValidationAttribute
{
    private readonly string[] _validRoles;

    public ValidRoleAttribute(params string[] validRoles)
    {
        _validRoles = validRoles;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || !_validRoles.Contains(value.ToString()))
        {
            return new ValidationResult($"Invalid role. Allowed roles are: {string.Join(", ", _validRoles)}");
        }

        return ValidationResult.Success;
    }
}
