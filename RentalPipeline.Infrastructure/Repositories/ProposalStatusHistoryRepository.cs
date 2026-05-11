using Microsoft.EntityFrameworkCore;
using RentalPipeline.Application.Interfaces.Repositories;
using RentalPipeline.Domain.Models;
using RentalPipeline.Infrastructure.Persistence;

namespace RentalPipeline.Infrastructure.Repositories;

public class ProposalStatusHistoryRepository : Repository<ProposalStatusHistory>, IProposalStatusHistoryRepository
{
    public ProposalStatusHistoryRepository( RentalDbContext context) : base(context)
    {
    }
    public async Task<IEnumerable<ProposalStatusHistory>> GetByProposalIdAsync(int proposalId)
    {
        return await Context.ProposalStatusHistories
            .Where(x => x.ProposalId == proposalId)
            .OrderBy(x => x.ChangedAt)
            .ToListAsync();
    }
}