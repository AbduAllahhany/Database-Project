using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hospital.management.system.BLL.Models.Patient;

public class PatientEmergancyContact
{
 
    public Guid Id { get; set; }
    
    public string PatientName { get; set; }


    public string Name { get; set; } = null!;


    public string? Phone { get; set; }


    public string Relationship { get; set; } = null!;
}