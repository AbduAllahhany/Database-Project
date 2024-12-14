using System.ComponentModel.DataAnnotations;
using hospital.management.system.BLL.Filters;

namespace hospital.management.system.BLL.Models.Admin;

public class ChooseRoomModel
{
    public Guid? PatientId { get; set; }
    [Required] public Guid? RoomId { get; set; }
    [Required] public DateOnly? StartDate { get; set; }
    [Required][IncomingDate] public DateOnly? EndDate { get; set; }

    public IEnumerable<AvailableRoomsModel> AvailableRooms { get; set; } = new List<AvailableRoomsModel>();
}