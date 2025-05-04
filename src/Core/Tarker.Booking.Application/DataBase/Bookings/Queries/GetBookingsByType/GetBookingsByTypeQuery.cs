using System;
using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.DataBase.Bookings.Queries.GetBookingsByType;

public class GetBookingsByTypeQuery : IGetBookingsByTypeQuery
{
    private readonly IDataBaseService _dataBaseService;
    public GetBookingsByTypeQuery(IDataBaseService dataBaseService)
    {
        _dataBaseService = dataBaseService;
    }

    public async Task<List<GetBookingsByTypeModel>> Execute(string type){
        // var query = _dataBaseService.Booking
        //     .Join(_dataBaseService.Customer,
        //         booking => booking.CustomerId,
        //         customer => customer.CustomerId,
        //         (booking, customer) => new {booking, customer})
        //     .Where(r => r.booking.Type == type)
        //     .Select(r => new GetBookingsByTypeModel
        //     {
        //         RegisterDate = r.booking.RegisterDate,
        //         Code = r.booking.Code,
        //         Type = r.booking.Type,
        //         CustomerFullName = r.customer.FullName,
        //         CustomerDocumentNumber = r.customer.DocumentNumber
        //     });

        var query = (from booking in _dataBaseService.Booking
                    join customer in _dataBaseService.Customer 
                    on booking.CustomerId equals customer.CustomerId
                    where booking.Type == type
                    select new GetBookingsByTypeModel
                    {
                        RegisterDate = booking.RegisterDate,
                        Code = booking.Code,
                        Type = booking.Type,
                        CustomerFullName = customer.FullName,
                        CustomerDocumentNumber = customer.DocumentNumber
                    });
        return await query.ToListAsync();

    }
}
