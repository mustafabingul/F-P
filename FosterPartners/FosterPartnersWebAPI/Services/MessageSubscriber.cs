using System.Text;
using FosterPartnersWebAPI.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FosterPartnersWebAPI.Services;

public class MessageSubscriber : IMessageSubscriber
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string ExchangeName = "rabbitMQExchange";
    private const string QueueName = "rabbitMQQueue";

    public MessageSubscriber()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
        _channel.QueueDeclare(QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBind(QueueName, ExchangeName, routingKey: "");
    }

    public void Subscribe(Action<string> onProgressUpdated)
    {
        var message = string.Empty;
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, eventArgs) =>
        {
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            onProgressUpdated(message);
            Console.WriteLine("MESSAGEEE::: " + message);
        };

        _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);
    }

    public void Dispose()
    {
        _connection.Dispose();
        _channel.Dispose();
    }
}