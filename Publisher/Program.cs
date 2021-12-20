﻿// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

var counter =0;

Console.WriteLine("Hello, Publisher!");
do
{
    var timeToSleep = new Random().Next(1000, 3000);
    Thread.Sleep(timeToSleep);

    var factory = new ConnectionFactory() { HostName = "localhost" };
    using (var connection = factory.CreateConnection())
    using (var channel = connection.CreateModel())
    {
         channel.QueueDeclare(queue: "dev-queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
         var message = $"Messahe from publisher N:[{counter++}]";

         var body =Encoding.UTF8.GetBytes(message);

         channel.BasicPublish(exchange:"", routingKey:"dev-queue",basicProperties:null,body:body);

         Console.WriteLine($"Message is sent into Default Exchange N:[{counter++}]");
    }
} while (true);
