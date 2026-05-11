using System.ComponentModel.DataAnnotations;
using static RentalPipeline.Domain.Models.RentalProposal;

namespace RentalPipeline.Application.DTOs;

public class UpdateProposalStatusDto
{
    [Required]
    public ProposalStatus Status { get; set; }
}