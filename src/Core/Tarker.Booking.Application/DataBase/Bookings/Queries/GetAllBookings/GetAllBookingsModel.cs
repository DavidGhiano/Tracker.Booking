using System;

namespace Tarker.Booking.Application.DataBase.Bookings.Queries.GetAllBookings;

public class GetAllBookingsModel
{
    public int BookingId { get; set; }
    public DateTime RegisterDate { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string CustomerFullName { get; set; } = string.Empty;
    public string CustomerDocumentNumber { get; set; } = string.Empty;

}
