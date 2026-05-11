using System.ComponentModel.DataAnnotations;
using static RentalPipeline.Domain.Models.Property;

namespace RentalPipeline.Application.DTOs;

public class CreatePropertyDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    [Required]
    [MaxLength(500)]
    public string Description { get; set; }
    [Required]
    [MaxLength(200)]
    public string Address { get; set; }
    [Required]
    public PropertyType Type { get; set; }
}