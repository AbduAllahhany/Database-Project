 

namespace hospital.management.system.DAL;

public class SD
{
    public const string Admin = "Admin";
    public const string SuperAdmin = "SuperAdmin";
    public const string Patient = "Patient";
    public const string Nurse = "Nurse";
    public const string Doctor = "Doctor";
    public const string Intern = "Intern";

    public const string Confirmed = "Confirmed";

    public static readonly IEnumerable<string> ValidSpecializations =
        new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Cardiology",
            "Neurology",
            "Orthopedics",
            "Pediatrics",
            "Dermatology",
            "Psychiatry",
            "General Surgery",
            "Radiology",
            "Anesthesiology",
            "Oncology"
        };

    public static readonly IEnumerable<string> ValidDepartments = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "Emergency",
        "Cardiology",
        "Neurology",
        "Orthopedics",
        "Pediatrics",
        "Dermatology",
        "Psychiatry",
        "General Surgery",
        "Radiology",
        "Anesthesiology",
        "Oncology",
        "Gynecology",
        "Urology",
        "Nephrology",
        "Pathology"
    };

    public static
        Dictionary<string, Guid> departments = new Dictionary<string, Guid>
        {
            { "Emergency", Guid.NewGuid() },
            { "Cardiology", Guid.NewGuid() },
            { "Neurology", Guid.NewGuid() },
            { "Orthopedics", Guid.NewGuid() },
            { "Pediatrics", Guid.NewGuid() },
            { "Dermatology", Guid.NewGuid() },
            { "Psychiatry", Guid.NewGuid() },
            { "General Surgery", Guid.NewGuid() },
            { "Radiology", Guid.NewGuid() },
            { "Anesthesiology", Guid.NewGuid() },
            { "Oncology", Guid.NewGuid() },
            { "Gynecology", Guid.NewGuid() },
            { "Urology", Guid.NewGuid() },
            { "Nephrology", Guid.NewGuid() },
            { "Pathology", Guid.NewGuid() }
        };

    public const string Success = "Success";
    public const string Error = "Error";
}

