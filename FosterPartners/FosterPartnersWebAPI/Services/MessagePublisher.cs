using System.Text;
using FosterPartnersWebAPI.Services.Interfaces;
using RabbitMQ.Client;

namespace FosterPartnersWebAPI.Services;

public class MessagePublisher : IMessagePublisher
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string ExchangeName = "rabbitMQExchange";
    private const string QueueName = "rabbitMQQueue";
    
    public MessagePublisher()
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
    
    public void PublishStartMessage(Guid taskId)
    {
        var message = Encoding.UTF8.GetBytes(taskId.ToString());
        _channel.BasicPublish(ExchangeName, routingKey: "", basicProperties: null, body: message);
    }
    
    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}