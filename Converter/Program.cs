using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "127.0.0.1" };

var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

const string queueName = "convertPDF";
const string directory = @"C:\Users\Leonardo\Documents\Pessoal\Projetos\DocToPDF\web\wwwroot\";

channel.QueueDeclare(queueName, exclusive: false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    string fileName =  Encoding.UTF8.GetString(body);
    Console.WriteLine("Arquivo recebido");

    string filePath = directory + fileName;
    ConvertPDF.Convert(filePath);
    Console.WriteLine("Arquivo convertido");
};

channel.BasicConsume(queueName, autoAck: true, consumer);

Console.WriteLine("Tecle para encerrar");
Console.ReadLine();


