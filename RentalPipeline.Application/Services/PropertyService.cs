using RentalPipeline.Application.DTOs;
using RentalPipeline.Application.Interfaces.Repositories;
using RentalPipeline.Application.Interfaces.Services;
using RentalPipeline.Domain.Exceptions;
using RentalPipeline.Domain.Models;

namespace RentalPipeline.Application.Services;

public class PropertyService(IRepository<Property> _repository, 
                             PropertyMapper mapper) : IPropertyService
{
    public async Task CreateAsync(CreatePropertyDto dto)
    {
        var property = mapper.ToModel(dto);
        await _repository.InsertAsync(property);
    }

    public async Task<PropertyResponseDto> GetByIdAsync(int id)
    {
        var property = await _repository.GetByIdAsync(id);
        if (property is null)
            throw new NotFoundException("Property not found");

        return mapper.ToResponse(property);
    }
}