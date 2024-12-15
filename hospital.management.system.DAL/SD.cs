 

namespace hospital.management.system.DAL;

public class SD
{
    public const string Admin = "Admin";
    public const string SuperAdmin = "SuperAdmin";
    public const string Patient = "Patient";
    public const string OfficeBoy = "Officeboy";
    public const string Nurse = "Nurse";
    public const string Doctor = "Doctor";
    public const string Intern = "Intern";

    public const string Confirmed = "Confirmed";
    public const string Pending = "Pending";
    public const string Rejected = "Rejected";

    public static readonly IEnumerable<string> Roles =
        new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            SD.Intern,
            SD.Nurse,
            SD.OfficeBoy,
        };

    public static readonly IEnumerable<string> Roles =
        new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            SD.Intern,
            SD.Nurse,
            SD.OfficeBoy,
        };

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

    /// <summary>
    /// 
    /// </summary>
    public static Dictionary<string, Guid> Departments = new Dictionary<string, Guid>();

    public const string Success = "Success";
    public const string Error = "Error";
}

