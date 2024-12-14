using System.ComponentModel.DataAnnotations;

namespace hospital.management.system.BLL.Models.Admin;

public class AvailableRoomsModel
{
    [Required] public Guid? Id { get; set; }
    [Required] public decimal? CostPerDay { get; set; }
    [Required] public string? Type { get; set; }
    [Required] public int? RoomNumber { get; set; }
}