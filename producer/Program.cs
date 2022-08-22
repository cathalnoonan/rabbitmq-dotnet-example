using RabbitMQ.Client;

namespace producer;

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

        var message = GetMessage(args);
        var body = System.Text.Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: "",
            routingKey: QUEUE_NAME,
            basicProperties: null,
            body: body
        );

        System.Console.WriteLine($" [x] Sent message: '{message}' to the queue: '{QUEUE_NAME}'.");

        channel.Close();
        connection.Close();
    }

    private static string GetMessage(string[] args)
    {
        if (args.Length > 0)
        {
            return string.Join(" ", args);
        }

        // As a default, send hard-coded message to the queue.
        return "Hello, World!";
    }
}
