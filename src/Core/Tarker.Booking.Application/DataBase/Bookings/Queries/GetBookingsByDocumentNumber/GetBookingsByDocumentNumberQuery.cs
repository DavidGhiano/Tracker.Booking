using System;
using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.DataBase.Bookings.Queries.GetBookingsByDocumentNumber;

public class GetBookingsByDocumentNumberQuery : IGetBookingsByDocumentNumberQuery
{
    private readonly IDataBaseService _dataBaseService;
    public GetBookingsByDocumentNumberQuery(IDataBaseService dataBaseService)
    {
        _dataBaseService = dataBaseService;
    }
    public async Task<List<GetBookingsByDocumentNumberModel>> Execute(string documentNumber)
    {
        // var bookings = await _dataBaseService.Booking
        //     .Join(_dataBaseService.Customer,
        //           booking => booking.CustomerId,
        //           customer => customer.CustomerId,
        //           (booking, customer) => new { booking, customer})
        //     .Where(booking => booking.customer.DocumentNumber == documentNumber)
        //     .Select(r=> new GetBookingsByDocumentNumberModel
        //     {
        //         RegisterDate = r.booking.RegisterDate,
        //         Code = r.booking.Code,
        //         Type = r.booking.Type
        //     })
        //     .ToListAsync();
        var query = from booking in _dataBaseService.Booking
                    join customer in _dataBaseService.Customer
                    on booking.CustomerId equals customer.CustomerId
                    where customer.DocumentNumber == documentNumber
                    select new GetBookingsByDocumentNumberModel
                    {
                        RegisterDate = booking.RegisterDate,
                        Code = booking.Code,
                        Type = booking.Type
                    };

        var bookings = await query.ToListAsync();
        return bookings;
    }
}
