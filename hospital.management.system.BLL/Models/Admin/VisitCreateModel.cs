using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;

namespace hospital.management.system.BLL.Models.Admin;

public class VisitCreateModel
{
    [Required] public string Notes { get; set; }


    [Required] public string Reason { get; set; }

    [AppointmentDate] public DateOnly Date { get; set; }

    [Required] public Guid PatientId { get; set; }


}