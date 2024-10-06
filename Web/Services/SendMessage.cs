using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Web.Services;

public class SendMessage : ISendMessage
{
    public void Send(string fileName)
    {
        var factory = new ConnectionFactory { HostName = "127.0.0.1" };
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        const string queueName = "convertPDF";
        var queue = channel.QueueDeclare(queueName, 
                                        exclusive: false);

        var body = Encoding.UTF8.GetBytes(fileName);
        channel.BasicPublish(exchange: string.Empty, routingKey: queueName, body: body);
    }
}