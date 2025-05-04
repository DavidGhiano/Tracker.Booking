using System;
using FluentValidation;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUserPassword;

namespace Tarker.Booking.Application.Validators.User;

public class UpdateUserPasswordValidator : AbstractValidator<UpdateUserPasswordModel>
{
    public UpdateUserPasswordValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .GreaterThan(0);
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MaximumLength(10);
    }
}