using System.ComponentModel.DataAnnotations;

namespace RentalPipeline.Application.DTOs;

public class CreateCustomerDto
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; }

    [Required]
    [MaxLength(20)]
    public string DocumentNumber { get; set; }

    [Required]
    [Phone]
    [MaxLength(20)]
    public string PhoneNumber { get; set; }
}