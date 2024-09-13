using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "127.0.0.1" };

var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

const string queueName = "convertPDF";
channel.QueueDeclare(queueName, exclusive: false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    string filePath = Encoding.UTF8.GetString(body);
    Console.WriteLine("Arquivo recebido");

    ConvertPDF.Convert(filePath);
    Console.WriteLine("Arquivo convertido");
};

channel.BasicConsume(queueName, autoAck: true, consumer);

bool loop = true;
do
{
    Console.WriteLine("Digite ESC para encerrar");
    var consoleKeyInfo = Console.ReadKey();

    if (consoleKeyInfo.Key == ConsoleKey.Escape)
        loop = false;

} while (loop);

