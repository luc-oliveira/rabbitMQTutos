using System;
using RabbitMQ.Client;
using System.Text;

class Send
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        var infos = Console.ReadLine();
        while (infos != "exit")
        {
            SendMessage(factory, infos);
            Console.WriteLine(" Write [exit] to exit.");
            infos = Console.ReadLine();
        }

        //Console.ReadLine();
    }

    public static void SendMessage(ConnectionFactory factory, string message)
    {
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            //string message = "Hello My Friendo!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }
    }
}