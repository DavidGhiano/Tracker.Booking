using System;

namespace Tarker.Booking.Application.DataBase.Customer.Commands.CreateCustomer;

public class CreateCustomerModel
{
    public string FullName { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
}
