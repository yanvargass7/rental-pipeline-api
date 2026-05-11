using RentalPipeline.Domain.Exceptions;

namespace RentalPipeline.Domain.Models;

public class RentalProposal
{
    public int Id { get; private set; }
    public int PropertyId { get; private set; }
    public Property Property { get; private set; }
    public int CustomerId { get; private set; }
    public Customer Customer { get; private set; }
    public ProposalStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public RentalProposal(int propertyId, int customerId)
    {
        PropertyId = propertyId;
        CustomerId = customerId;
        Status = ProposalStatus.New;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Proposal workflow status.
    /// </summary>
    public enum ProposalStatus
    {
        /// <summary>
        /// Proposal created.
        /// </summary>
        New = 1,

        /// <summary>
        /// Credit analysis in progress.
        /// </summary>
        CreditAnalysis = 2,

        /// <summary>
        /// Contract generated.
        /// </summary>
        ContractIssued = 3,

        /// <summary>
        /// Contract signed by customer.
        /// </summary>
        Signed = 4,

        /// <summary>
        /// Contract activated.
        /// </summary>
        Active = 5,

        /// <summary>
        /// Proposal rejected.
        /// </summary>
        Rejected = 6,

        /// <summary>
        /// Proposal cancelled.
        /// </summary>
        Cancelled = 7
    }

    public void MoveTo(ProposalStatus newStatus)
    {
        var allowedTransitions =
            new Dictionary<ProposalStatus, ProposalStatus[]>
            {
                [ProposalStatus.New] =
                [
                    ProposalStatus.CreditAnalysis,
                    ProposalStatus.Rejected,
                    ProposalStatus.Cancelled
                ],

                [ProposalStatus.CreditAnalysis] =
                [
                    ProposalStatus.ContractIssued,
                    ProposalStatus.Rejected,
                    ProposalStatus.Cancelled
                ],

                [ProposalStatus.ContractIssued] =
                [
                    ProposalStatus.Signed,
                    ProposalStatus.Rejected,
                    ProposalStatus.Cancelled
                ],

                [ProposalStatus.Signed] =
                [
                    ProposalStatus.Active,
                    ProposalStatus.Rejected,
                    ProposalStatus.Cancelled
                ]
            };

        if (!allowedTransitions.ContainsKey(Status))
        {
            throw new BusinessRuleException("No transitions allowed");
        }

        var validTransitions = allowedTransitions[Status];

        if (!validTransitions.Contains(newStatus))
        {
            throw new BusinessRuleException(
                $"Invalid transition: {Status} -> {newStatus}");
        }

        Status = newStatus;
    }
}