using RentalPipeline.Application.DTOs;
using RentalPipeline.Application.Interfaces.Repositories;
using RentalPipeline.Application.Interfaces.Services;
using RentalPipeline.Application.Mappings;
using RentalPipeline.Domain.Exceptions;
using RentalPipeline.Domain.Models;

namespace RentalPipeline.Application.Services;

public class CustomerService(IRepository<Customer> _repository, 
                             CustomerMapper mapper) : ICustomerService
{
    public async Task CreateAsync(CreateCustomerDto dto)
    {
        var customer = mapper.ToModel(dto);
        await _repository.InsertAsync(customer);
    }

    public async Task<CustomerResponseDto> GetByIdAsync(int id)
    {
        var customer = await _repository.GetByIdAsync(id);

        if (customer is null)
            throw new NotFoundException("Customer not found");

        return mapper.ToResponse(customer);
    }
}