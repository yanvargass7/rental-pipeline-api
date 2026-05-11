using System.ComponentModel.DataAnnotations;

namespace RentalPipeline.Application.DTOs;

public class CreateRentalProposalDto
{
    [Required]
    public int PropertyId { get; set; }
    [Required]
    public int CustomerId { get; set; }
}