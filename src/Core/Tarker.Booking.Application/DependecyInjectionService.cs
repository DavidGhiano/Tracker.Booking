using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Tarker.Booking.Application.Configuration;
using Tarker.Booking.Application.DataBase.Bookings.Commands.CreateBooking;
using Tarker.Booking.Application.DataBase.Bookings.Queries.GetAllBookings;
using Tarker.Booking.Application.DataBase.Bookings.Queries.GetBookingsByDocumentNumber;
using Tarker.Booking.Application.DataBase.Bookings.Queries.GetBookingsByType;
using Tarker.Booking.Application.DataBase.Customer.Commands.CreateCustomer;
using Tarker.Booking.Application.DataBase.Customer.Commands.DeleteCustomer;
using Tarker.Booking.Application.DataBase.Customer.Commands.UpdateCustomer;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetAllCustomer;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetCustomerByDocumentNumber;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetCustommerByID;
using Tarker.Booking.Application.DataBase.User.Commands.CreateUser;
using Tarker.Booking.Application.DataBase.User.Commands.DeleteUser;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUser;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUserPassword;
using Tarker.Booking.Application.DataBase.User.Queries.GetAllUser;
using Tarker.Booking.Application.DataBase.User.Queries.GetUserById;
using Tarker.Booking.Application.DataBase.User.Queries.GetUserByUserNameAndPassword;
using Tarker.Booking.Application.Validators.Booking;
using Tarker.Booking.Application.Validators.Customer;
using Tarker.Booking.Application.Validators.User;

namespace Tarker.Booking.Application;

public static class DependencyInjectionService
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var mapper = new MapperConfiguration(config => 
        {
            config.AddProfile(new MapperProfile());
        });
        services.AddSingleton(mapper.CreateMapper());
        #region User
        services.AddTransient<ICreateUserCommand, CreateUserCommand>();
        services.AddTransient<IUpdateUserCommand, UpdateUserCommand>();
        services.AddTransient<IDeleteUserCommand, DeleteUserCommand>();
        services.AddTransient<IUpdateUserPasswordCommand, UpdateUserPasswordCommand>();
        services.AddTransient<IGetAllUserQuery, GetAllUserQuery>();
        services.AddTransient<IGetUserByIdQuery, GetUserByIdQuery>();
        services.AddTransient<IGetUserByUserNameAndPasswordQuery, GetUserByUserNameAndPasswordQuery>();
        #endregion
        #region Customer
        services.AddTransient<ICreateCustomerCommand, CreateCustomerCommand>();
        services.AddTransient<IUpdateCustomerCommand, UpdateCustomerCommand>();
        services.AddTransient<IDeleteCustomerCommand, DeleteCustomerCommand>();
        services.AddTransient<IGetAllCustomerQuery, GetAllCustomerQuery>();
        services.AddTransient<IGetCustomerByIdQuery, GetCustomerByIdQuery>();
        services.AddTransient<IGetCustomerByDocumentNumberQuery, GetCustomerByDocumentNumberQuery>();
        #endregion
        #region Booking
        services.AddTransient<ICreateBookingCommand, CreateBookingCommand>();
        services.AddTransient<IGetAllBookingsQuery, GetAllBookingsQuery>();
        services.AddTransient<IGetBookingsByDocumentNumberQuery, GetBookingsByDocumentNumberQuery>();
        services.AddTransient<IGetBookingsByTypeQuery, GetBookingsByTypeQuery>();
        #endregion
        #region Validator
        services.AddScoped<IValidator<CreateUserModel>, CreateUserValidator>();
        services.AddScoped<IValidator<UpdateUserModel>, UpdateUserValidator>();
        services.AddScoped<IValidator<UpdateUserPasswordModel>, UpdateUserPasswordValidator>();
        services.AddScoped<IValidator<(string, string)>, GetUserByUserNameAndPasswordValidator>();
        services.AddScoped<IValidator<CreateCustomerModel>, CreateCustomerValidator>();
        services.AddScoped<IValidator<UpdateCustomerModel>, UpdateCustomerValidator>();
        services.AddScoped<IValidator<CreateBookingModel>, CreateBookingValidator>();
        #endregion
        
        return services;
    }
}