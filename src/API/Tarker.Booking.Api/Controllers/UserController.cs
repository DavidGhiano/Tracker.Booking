using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.DataBase.User.Commands.CreateUser;
using Tarker.Booking.Application.DataBase.User.Commands.DeleteUser;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUser;
using Tarker.Booking.Application.DataBase.User.Commands.UpdateUserPassword;
using Tarker.Booking.Application.DataBase.User.Queries.GetAllUser;
using Tarker.Booking.Application.DataBase.User.Queries.GetUserById;
using Tarker.Booking.Application.DataBase.User.Queries.GetUserByUserNameAndPassword;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.External.ApplicationInsights;
using Tarker.Booking.Application.External.GetTokenJwt;
using Tarker.Booking.Application.Features;
using Tarker.Booking.Common.Constants;
using Tarker.Booking.Domain.Models.ApplicationInsights;

namespace Tarker.Booking.Api.Controllers
{
    #pragma warning disable
    [Authorize]
    [Route("api/v1/user")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class UserController : ControllerBase
    {
        private readonly IInsertApplicationInsightsService _insertApplicationInsightsService;
        public UserController(IInsertApplicationInsightsService insertApplicationInsightsService)
        {
            _insertApplicationInsightsService = insertApplicationInsightsService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreateUserModel model,
            [FromServices] ICreateUserCommand creacteUserCommand,
            [FromServices] IValidator<CreateUserModel> validator
        )
        {
            var validate = await validator.ValidateAsync(model);
            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest,
                    data: validate.Errors
                ));
            var data = await creacteUserCommand.Execute(model);
            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(
                StatusCodes.Status201Created,
                data: data,
                message: "User created successfully"
            ));
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateUserModel model,
            [FromServices] IUpdateUserCommand updateUserCommand,
            [FromServices] IValidator<UpdateUserModel> validator
        )
        {
            var validate = await validator.ValidateAsync(model);
            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest,
                    data: validate.Errors
                ));
            var data = await updateUserCommand.Execute(model);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK,
                data: data,
                message: "User updated successfully"
            ));
        }
        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword(
            [FromBody] UpdateUserPasswordModel model,
            [FromServices] IUpdateUserPasswordCommand updateUserPasswordCommand,
            [FromServices] IValidator<UpdateUserPasswordModel> validator
        )
        {
            var validate = await validator.ValidateAsync(model);
            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest,
                    data: validate.Errors
                ));
            var data = await updateUserPasswordCommand.Execute(model);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK,
                data: data,
                message: "User password updated successfully"
            ));
        }
        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> Delete(
            int userId,
            [FromServices] IDeleteUserCommand deleteUserCommand
        )
        {
            if (userId == 0)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest,
                    message: "User id is required"
                ));
            var data = await deleteUserCommand.Execute(userId);
            if (!data)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound,
                    data: data,
                    message: "User not found"
                ));
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK,
                data: data,
                message: "User deleted successfully"
            ));
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetAllUserQuery getAllUserQuery
        )
        {
            var data = await getAllUserQuery.Execute();
            if (data == null || data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound,
                    message: "Users not found"
                ));
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK,
                data: data,
                message: "Users retrieved successfully"
            ));
        }
        [HttpGet("get-by-id/{userId}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int userId,
            [FromServices] IGetUserByIdQuery getUserByIdQuery
        )
        {
            if (userId == 0)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest,
                    message: "User id is required"
                ));
            var data = await getUserByIdQuery.Execute(userId);
            if (data == null)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound,
                    message: "User not found"
                ));
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK,
                data: data,
                message: "User retrieved successfully"
            ));
        }
        [AllowAnonymous]
        [HttpGet("get-by-username-password/{userName}/{password}")]
        public async Task<IActionResult> GetByUsernamePassword(
            [FromRoute] string userName, [FromRoute] string password,
            [FromServices] IGetUserByUserNameAndPasswordQuery getUserByUserNameAndPasswordQuery,
            [FromServices] IValidator<(string, string)> validator,
            [FromServices] IGetTokenJwtService getTokenJwtService
        )
        {
            InsertApplicationInsightsModel metric = new(
                ApplicationsInsightsConstants.METRIC_TYPE_API_CALL,
                EntitiesConstants.USER,
                "GetByUsernamePassword");
            _insertApplicationInsightsService.Execute(metric);

            var validate = await validator.ValidateAsync((userName, password));
            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest,
                    data: validate.Errors
                ));
            var data = await getUserByUserNameAndPasswordQuery.Execute(userName, password);
            if (data == null)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound,
                    message: "User not found"
                ));
            data.Token = getTokenJwtService.Execute(data.UserId.ToString());
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK,
                data: data,
                message: "User retrieved successfully"
            ));
        }
    }
}
