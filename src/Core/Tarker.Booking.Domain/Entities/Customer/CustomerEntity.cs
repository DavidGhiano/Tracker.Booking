using System;
using Tarker.Booking.Domain.Entities.Booking;

namespace Tarker.Booking.Domain.Entities.Customer;

public class CustomerEntity
{
    public int CustomerId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public ICollection<BookingEntity> Bookings { get; set; } = new List<BookingEntity>();
}
