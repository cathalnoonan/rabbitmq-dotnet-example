using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace consumer;

public class Program
{
    private const string QUEUE_NAME = "hello";

    public static void Main(params string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: QUEUE_NAME,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = System.Text.Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
        };

        channel.BasicConsume(queue: QUEUE_NAME, autoAck: true, consumer: consumer);

        // Only keep the console open if no arguments are passed.
        // Include this for CI purposes.
        if (args.Length == 0)
        {
            System.Console.WriteLine("Press enter to close...");
            System.Console.ReadLine();
        }

        channel.Close();
        connection.Close();
    }
}
