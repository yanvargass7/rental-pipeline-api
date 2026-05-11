using static RentalPipeline.Domain.Models.RentalProposal;

namespace RentalPipeline.Domain.Models;

public class ProposalStatusHistory
{
    public int Id { get; private set; }
    public int ProposalId { get; private set; }
    public ProposalStatus PreviousStatus { get; private set; }
    public ProposalStatus CurrentStatus { get; private set; }
    public DateTime ChangedAt { get; private set; }

    public ProposalStatusHistory(
        int proposalId,
        ProposalStatus previousStatus,
        ProposalStatus currentStatus)
    {
        ProposalId = proposalId;
        PreviousStatus = previousStatus;
        CurrentStatus = currentStatus;
        ChangedAt = DateTime.UtcNow;
    }
}