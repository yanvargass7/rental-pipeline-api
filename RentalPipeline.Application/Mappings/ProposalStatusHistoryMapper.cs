using RentalPipeline.Application.DTOs;
using RentalPipeline.Domain.Models;
using static RentalPipeline.Domain.Models.RentalProposal;

namespace RentalPipeline.Application.Mappings;

public class ProposalStatusHistoryMapper
{
    public ProposalStatusHistory ToModel( int proposalId, ProposalStatus previousStatus, ProposalStatus currentStatus)
    {
        return new ProposalStatusHistory(proposalId, previousStatus, currentStatus);
    }
    public ProposalHistoryResponseDto ToResponse(ProposalStatusHistory history)
    {
        return new ProposalHistoryResponseDto
        {
            PreviousStatus = history.PreviousStatus.ToString(),
            CurrentStatus = history.CurrentStatus.ToString(),
            ChangedAt = history.ChangedAt
        };
    }
}