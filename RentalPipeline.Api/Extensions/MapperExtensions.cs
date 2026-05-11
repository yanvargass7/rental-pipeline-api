using RentalPipeline.Application.Mappings;

namespace RentalPipeline.API.Extensions;

public static class MapperExtensions
{
    public static IServiceCollection AddMappings(
        this IServiceCollection services)
    {
        services.AddScoped<PropertyMapper>();

        services.AddScoped<CustomerMapper>();

        services.AddScoped<RentalProposalMapper>();

        services.AddScoped<ProposalStatusHistoryMapper>();

        return services;
    }
}