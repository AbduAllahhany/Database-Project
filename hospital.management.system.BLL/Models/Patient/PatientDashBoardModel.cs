using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hospital.management.system.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.BLL.Models.Patient;

public class PatientDashBoardModel
{
    public system.Models.Entities.Patient Patient { get; set; } = null!;

    public IEnumerable<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();

    public IEnumerable<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public IEnumerable<Appointment> Appointments { get; set; } = new List<Appointment>();

    public IEnumerable<Bill> Bills { get; set; } = new List<Bill>();

    public IEnumerable<Visit> Visits { get; set; } = new List<Visit>();

    public IEnumerable<Admission> Admissions { get; set; } = new List<Admission>();
}
