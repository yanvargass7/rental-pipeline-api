using RentalPipeline.Application.DTOs;
namespace RentalPipeline.Application.Interfaces.Services;

public interface IPropertyService
{
    Task CreateAsync(CreatePropertyDto dto);
    Task<PropertyResponseDto> GetByIdAsync(int id);
}