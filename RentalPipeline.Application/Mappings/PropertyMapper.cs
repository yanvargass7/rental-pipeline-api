using RentalPipeline.Application.DTOs;
using RentalPipeline.Domain.Models;

public class PropertyMapper
{
    public Property ToModel(CreatePropertyDto dto) =>
    new(
        dto.Title,
        dto.Description,
        dto.Address,
        dto.Type);

    public PropertyResponseDto ToResponse(Property property)
    {
        return new PropertyResponseDto
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            Address = property.Address,
        };
    }
}