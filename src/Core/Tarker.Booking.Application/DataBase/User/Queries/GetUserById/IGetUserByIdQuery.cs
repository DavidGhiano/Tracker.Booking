using System;

namespace Tarker.Booking.Application.DataBase.User.Queries.GetUserById;

public interface IGetUserByIdQuery
{   
    Task<GetUserByIdModel> Execute(int id);
}
