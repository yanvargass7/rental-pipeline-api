using RentalPipeline.Application.DTOs;
using RentalPipeline.Domain.Models;

namespace RentalPipeline.Application.Mappings;

public class RentalProposalMapper
{
    public RentalProposal ToModel(CreateRentalProposalDto dto) => 
    new(
        dto.PropertyId, 
        dto.CustomerId);
}