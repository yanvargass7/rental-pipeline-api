namespace RentalPipeline.Application.Events;

public class ContractActivatedEvent
{
    public int ProposalId { get; set; }
    public int PropertyId { get; set; }
    public int CustomerId { get; set; }
    public DateTime ActivatedAt { get; set; }
}