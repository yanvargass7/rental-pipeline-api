using RentalPipeline.Application.Interfaces.Services;
using RentalPipeline.Application.Services;

namespace RentalPipeline.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddScoped<
            IPropertyService,
            PropertyService>();

        services.AddScoped<
            ICustomerService,
            CustomerService>();

        services.AddScoped<
            IRentalProposalService,
            RentalProposalService>();

        return services;
    }
}