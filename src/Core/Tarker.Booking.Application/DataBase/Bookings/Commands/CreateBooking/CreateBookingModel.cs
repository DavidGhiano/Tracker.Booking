using System;

namespace Tarker.Booking.Application.DataBase.Bookings.Commands.CreateBooking;

public class CreateBookingModel
{
    public string Code { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public int UserId { get; set; } 
}
