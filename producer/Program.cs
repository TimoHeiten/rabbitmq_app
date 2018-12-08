using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace producer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare("hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                        string message = "Hello World from the queue";
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                        Console.WriteLine(" [x] Sent {0}", message);
                    }
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
            catch (BrokerUnreachableException ex)
            {
                System.Console.WriteLine(ex.InnerException);
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex.Data.Keys);
                System.Console.WriteLine(ex.ToString());
            }
        }
    }
}
