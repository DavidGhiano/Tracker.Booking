using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tarker.Booking.Application.DataBase.Bookings.Commands.CreateBooking;
using Tarker.Booking.Application.DataBase.Bookings.Queries.GetAllBookings;
using Tarker.Booking.Application.DataBase.Bookings.Queries.GetBookingsByDocumentNumber;
using Tarker.Booking.Application.DataBase.Bookings.Queries.GetBookingsByType;
using Tarker.Booking.Application.Exceptions;
using Tarker.Booking.Application.Features;

namespace Tarker.Booking.Api.Controllers
{
    [Route("api/v1/booking")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManager))]
    public class BookingController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreateBookingModel model,
            [FromServices] ICreateBookingCommand createBookingCommand,
            [FromServices] IValidator<CreateBookingModel> validator
        )
        {
            var validate = await validator.ValidateAsync(model);
            if (!validate.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest,
                    data: validate.Errors
                ));
            var data = await createBookingCommand.Execute(model);
            return StatusCode(StatusCodes.Status201Created, ResponseApiService.Response(
                StatusCodes.Status201Created,
                data: data,
                message: "Booking created successfully"));
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromServices] IGetAllBookingsQuery getAllBookingsQuery)
        {
            var data = await getAllBookingsQuery.Execute();
            if(data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound));
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK,
                data: data,
                message: "Bookings retrieved successfully"));
        }
        [HttpGet("get-by-documentNumber")]
        public async Task<IActionResult> GetByDocumentNumber(
            [FromQuery] string documentNumber,
            [FromServices] IGetBookingsByDocumentNumberQuery getBookingsByDocumentNumberQuery
        )
        {
            if(string.IsNullOrEmpty(documentNumber))
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest,
                    message: "Document number is required"));
            var data = await getBookingsByDocumentNumberQuery.Execute(documentNumber);
            if (data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound));
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK,
                data: data,
                message: "Bookings retrieved successfully"));
        }
        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType(
            [FromQuery] string type,
            [FromServices] IGetBookingsByTypeQuery getBookingsByTypeQuery
        )
        {
            if(string.IsNullOrEmpty(type))
                return StatusCode(StatusCodes.Status400BadRequest, ResponseApiService.Response(
                    StatusCodes.Status400BadRequest,
                    message: "Type is required"));
            var data = await getBookingsByTypeQuery.Execute(type);
            if (data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound, ResponseApiService.Response(
                    StatusCodes.Status404NotFound));
            return StatusCode(StatusCodes.Status200OK, ResponseApiService.Response(
                StatusCodes.Status200OK,
                data: data,
                message: "Bookings retrieved successfully"));   
        }
    }
}
