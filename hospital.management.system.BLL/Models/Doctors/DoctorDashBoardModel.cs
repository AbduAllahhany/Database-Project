using hospital.management.system.Models.Entities;

public class DoctorDashBoardModel
{
    public string DoctorName { get; set; }
    public string Specialization { get; set; }
    public string DepartmentName { get; set; }
    public int WorkingHours { get; set; }
    
    // Updated to List to hold multiple medical records and appointments
    public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    public List<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}