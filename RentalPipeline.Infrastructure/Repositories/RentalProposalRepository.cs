using RentalPipeline.Application.Interfaces.Repositories;
using RentalPipeline.Domain.Models;
using RentalPipeline.Infrastructure.Persistence;

namespace RentalPipeline.Infrastructure.Repositories;

public class RentalProposalRepository(
    RentalDbContext context)
    : Repository<RentalProposal>(context), IRentalProposalRepository
{
}