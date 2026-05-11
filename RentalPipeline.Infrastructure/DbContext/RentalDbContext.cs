using Microsoft.EntityFrameworkCore;
using RentalPipeline.Domain.Models;

namespace RentalPipeline.Infrastructure.Persistence;

public class RentalDbContext : DbContext
{
    public RentalDbContext(DbContextOptions<RentalDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<RentalProposal> RentalProposals => Set<RentalProposal>();
    public DbSet<ProposalStatusHistory> ProposalStatusHistories => Set<ProposalStatusHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(RentalDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}