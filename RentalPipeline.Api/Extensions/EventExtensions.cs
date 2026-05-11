using RentalPipeline.Application.Interfaces.Events;
using RentalPipeline.Infrastructure.Events;

namespace RentalPipeline.API.Extensions;

public static class EventExtensions
{
    public static IServiceCollection AddEvents(
        this IServiceCollection services)
    {
        services.AddScoped<
            IEventPublisher,
            RabbitMqEventPublisher>();

        return services;
    }
}