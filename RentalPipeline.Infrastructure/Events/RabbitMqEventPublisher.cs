using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RentalPipeline.Application.Interfaces.Events;

namespace RentalPipeline.Infrastructure.Events;

public class RabbitMqEventPublisher : IEventPublisher
{
    public async Task PublishAsync<T>(T @event)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "contract-activated",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var json = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: "contract-activated",
            body: body);
    }
}