using RentalPipeline.Application.Interfaces;
using RentalPipeline.Application.Interfaces.Repositories;
using RentalPipeline.Infrastructure.Persistence;
using RentalPipeline.Infrastructure.Repositories;

namespace RentalPipeline.API.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped(
            typeof(IRepository<>),
            typeof(Repository<>));

        services.AddScoped<
            IPropertyRepository,
            PropertyRepository>();

        services.AddScoped<
            ICustomerRepository,
            CustomerRepository>();

        services.AddScoped<
            IRentalProposalRepository,
            RentalProposalRepository>();

        services.AddScoped<
            IProposalStatusHistoryRepository,
            ProposalStatusHistoryRepository>();

        services.AddScoped<
            IUnitOfWork,
            UnitOfWork>();

        return services;
    }
}