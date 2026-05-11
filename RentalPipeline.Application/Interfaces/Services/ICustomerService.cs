using RentalPipeline.Application.DTOs;
using RentalPipeline.Domain.Models;

namespace RentalPipeline.Application.Interfaces.Services;

public interface ICustomerService
{
    Task CreateAsync(CreateCustomerDto dto);
    Task<CustomerResponseDto> GetByIdAsync(int id);
}