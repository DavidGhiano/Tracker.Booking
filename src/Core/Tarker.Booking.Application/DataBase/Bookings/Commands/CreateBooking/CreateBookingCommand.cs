using System;
using AutoMapper;
using Tarker.Booking.Domain.Entities.Booking;

namespace Tarker.Booking.Application.DataBase.Bookings.Commands.CreateBooking;

public class CreateBookingCommand : ICreateBookingCommand
{
    private readonly IDataBaseService _dataBaseService;
    private readonly IMapper _mapper;

    public CreateBookingCommand(IDataBaseService dataBaseService, IMapper mapper)
    {
        _dataBaseService = dataBaseService;
        _mapper = mapper;
    }
    public async Task<CreateBookingModel> Execute(CreateBookingModel model)
    {
        var Booking = _mapper.Map<BookingEntity>(model);
        Booking.RegisterDate = DateTime.Now;
        await _dataBaseService.Booking.AddAsync(Booking);
        await _dataBaseService.SaveAsync();
        return model;
    }
}
