using System;

namespace Tarker.Booking.Application.DataBase.Bookings.Queries.GetAllBookings;

public interface IGetAllBookingsQuery
{
    Task<List<GetAllBookingsModel>> Execute();
}
