using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.DataBase.Customer.Queries.GetCustomerByDocumentNumber;

public class GetCustomerByDocumentNumberQuery : IGetCustomerByDocumentNumberQuery
{
    private readonly IDataBaseService _dataBaseService;
    private readonly IMapper _mapper;
    public GetCustomerByDocumentNumberQuery(IDataBaseService dataBaseService, IMapper mapper)
    {
        _dataBaseService = dataBaseService;
        _mapper = mapper;
    }
    public async Task<GetCustomerByDocumentNumberModel> Execute(string documentNumber)
    {
        var customer = await _dataBaseService.Customer.FirstOrDefaultAsync(c  => c.DocumentNumber == documentNumber);
        return _mapper.Map<GetCustomerByDocumentNumberModel>(customer);
    }
}
