using RentalPipeline.Application.DTOs;
using RentalPipeline.Domain.Models;

namespace RentalPipeline.Application.Mappings;

public class CustomerMapper
{
    public Customer ToModel(CreateCustomerDto dto) =>
    new(
        dto.Name,
        dto.Email,
        dto.DocumentNumber,
        dto.PhoneNumber);

    public CustomerResponseDto ToResponse(Customer customer)
    {
        return new CustomerResponseDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Document = customer.DocumentNumber,
            Phone = customer.PhoneNumber
        };
    }
}