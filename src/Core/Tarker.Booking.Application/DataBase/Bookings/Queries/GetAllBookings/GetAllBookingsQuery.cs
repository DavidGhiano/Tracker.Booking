using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.DataBase.Bookings.Queries.GetAllBookings;

public class GetAllBookingsQuery : IGetAllBookingsQuery
{
    private readonly IDataBaseService _dataBaseService;

    public GetAllBookingsQuery(IDataBaseService dataBaseService, IMapper mapper)
    {
        _dataBaseService = dataBaseService;
    }
    public async Task<List<GetAllBookingsModel>> Execute()
    {
        // Method Syntax
        // var bookings = await _dataBaseService.Booking
        //     .Join(_dataBaseService.Customer,
        //            booking => booking.CustomerId,
        //            customer => customer.CustomerId,
        //            (booking, customer) => new GetAllBookingsModel
        //            {
        //             BookingId = booking.BookingId,
        //             RegisterDate = booking.RegisterDate,
        //             Code = booking.Code,
        //             Type = booking.Type,
        //             CustomerFullName = customer.FullName,
        //             CustomerDocumentNumber = customer.DocumentNumber
        //            }).ToListAsync();
        // Query Syntax
        var bookings = await (from booking in _dataBaseService.Booking
                              join customer in _dataBaseService.Customer
                              on booking.CustomerId equals customer.CustomerId
                              select new GetAllBookingsModel
                              {
                                  BookingId = booking.BookingId,
                                  RegisterDate = booking.RegisterDate,
                                  Code = booking.Code,
                                  Type = booking.Type,
                                  CustomerFullName = customer.FullName,
                                  CustomerDocumentNumber = customer.DocumentNumber
                              }).ToListAsync();
        return bookings;
    }
}
