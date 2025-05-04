using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using Tarker.Booking.Application.DataBase.User.Commands.CreateUser;

namespace Tarker.Booking.Application.Validators.User;

public class CreateUserValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.LastName)
            .NotEmpty()
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50);
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MaximumLength(10);
    }
}
