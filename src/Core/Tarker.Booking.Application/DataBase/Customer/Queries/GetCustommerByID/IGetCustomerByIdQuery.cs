namespace Tarker.Booking.Application.DataBase.Customer.Queries.GetCustommerByID;

public interface IGetCustomerByIdQuery
{
    Task<GetCustomerByIdModel> Execute(int customerId);
}
