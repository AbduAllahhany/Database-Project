
using System.ComponentModel.DataAnnotations;


namespace hospital.management.system.BLL.Filters;


    public class AppointmentDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var date = (DateTime)value;
            return date > DateTime.UtcNow.AddHours(2) ? true : false;
        }
    }

