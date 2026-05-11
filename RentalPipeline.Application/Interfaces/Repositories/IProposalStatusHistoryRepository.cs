using RentalPipeline.Application.Interfaces.Repositories;
using RentalPipeline.Domain.Models;

public interface IProposalStatusHistoryRepository : IRepository<ProposalStatusHistory>
{
    Task<IEnumerable<ProposalStatusHistory>> GetByProposalIdAsync(int proposalId);
}