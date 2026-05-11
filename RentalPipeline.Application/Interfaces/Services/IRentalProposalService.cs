using RentalPipeline.Application.DTOs;
using RentalPipeline.Domain.Models;

namespace RentalPipeline.Application.Interfaces.Services;

public interface IRentalProposalService
{
    Task CreateAsync(CreateRentalProposalDto dto);
    Task UpdateStatusAsync(int proposalId, UpdateProposalStatusDto dto);
    Task<IEnumerable<ProposalHistoryResponseDto>>GetHistoryAsync(int proposalId);
}