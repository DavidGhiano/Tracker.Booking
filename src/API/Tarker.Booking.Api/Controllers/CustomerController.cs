using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.DataBase.Customer.Commands.CreateCustomer;
using Tarker.Booking.Application.DataBase.Customer.Commands.DeleteCustomer;
using Tarker.Booking.Application.DataBase.Customer.Commands.UpdateCustomer;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetAllCustomer;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetCustomerByDocumentNumber;
using Tarker.Booking.Application.DataBase.Customer.Queries.GetCustommerByID;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.Features;

namespace Tarker.Booking.Api.Controllers
{
    [Route("api/v1/customer")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class CustomerController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreateCustomerModel model,
            [FromServices] ICreateCustomerCommand createCustomerCommand,
            [FromServices] IValidator<CreateCustomerModel> validator
        )
        {
            var validate = await validator.ValidateAsync(model);
            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest,
                    data: validate.Errors
                ));
            var data = await createCustomerCommand.Execute(model);
            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(
                StatusCodes.Status201Created,
                data: data,
                message: "Customer created successfully"
            ));
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateCustomerModel model,
            [FromServices] IUpdateCustomerCommand updateCustomerCommand,
            [FromServices] IValidator<UpdateCustomerModel> validator
        )
        {
            var validate = await validator.ValidateAsync(model);
            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest,
                    data: validate.Errors
                ));
            var data = await updateCustomerCommand.Execute(model);
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK,
                data: data,
                message: "Customer updated successfully"
            ));
        }
        [HttpDelete("delete/{customerId}")]
        public async Task<IActionResult> Delete(
            [FromRoute] int customerId,
            [FromServices] IDeleteCustomerCommand deleteCustomerCommand
        )
        {
            if (customerId == 0)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest,
                    message: "Customer ID is required"
                ));
            var data = await deleteCustomerCommand.Execute(customerId);
            if (!data)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound,
                    message: "Customer not found"
                ));
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK,
                data: data,
                message: "Customer deleted successfully"
            ));
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetAllCustomerQuery getAllCustomerQuery
        )
        {
            var data = await getAllCustomerQuery.Execute();
            if (data == null || data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound,
                    message: "No customers found"
                ));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK,
                data: data,
                message: "Customers retrieved successfully"
            ));
        }
        [HttpGet("get-by-id/{customerId}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int customerId,
            [FromServices] IGetCustomerByIdQuery getCustomerByIdQuery
        )
        {
            if (customerId == 0)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest,
                    message: "Customer ID is required"
                ));
            var data = await getCustomerByIdQuery.Execute(customerId);
            if (data == null)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound,
                    message: "Customer not found"
                ));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK,
                data: data,
                message: "Customer retrieved successfully"
            ));
        }
        [HttpGet("get-by-documentNumber/{documentNumber}")]
        public async Task<IActionResult> GetByDocumentNumber(
            [FromRoute] string documentNumber,
            [FromServices] IGetCustomerByDocumentNumberQuery getCustomerByDocumentNumberQuery
        )
        {
            if (string.IsNullOrEmpty(documentNumber))
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest,
                    message: "Document number is required"
                ));
            var data = await getCustomerByDocumentNumberQuery.Execute(documentNumber);
            if (data == null)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound,
                    message: "Customer not found"
                ));

            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK,
                data: data,
                message: "Customer retrieved successfully"
            ));
        }
    }
}
