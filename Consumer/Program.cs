// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Hello, Consumer!");


    var factory = new ConnectionFactory() { HostName = "localhost" };
    using (var connection = factory.CreateConnection())
    using (var channel = connection.CreateModel())
    {
        channel.QueueDeclare(queue: "dev-queue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, args) =>
        {
            var body = args.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            Console.WriteLine($"Received message: {message}");
        };

        channel.BasicConsume(queue: "dev-queue", autoAck: true, consumer: consumer);
        Console.WriteLine($"Subscribed to the queue 'dev-queue'");
        Console.ReadLine();
    }
    

       