using System;

namespace Tarker.Booking.Application.DataBase.User.Commands.UpdateUser;

public interface IUpdateUserCommand
{
    Task<UpdateUserModel> Execute(UpdateUserModel model);
}
