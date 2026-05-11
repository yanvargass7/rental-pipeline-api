namespace RentalPipeline.Application.Interfaces.Events;

public interface IEventPublisher
{
    Task PublishAsync<T>(T @event);
}