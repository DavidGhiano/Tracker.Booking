using System;

namespace Tarker.Booking.Application.DataBase.Customer.Queries.GetCustommerByID;

public class GetCustomerByIdModel
{
    public int CustomerId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
}
