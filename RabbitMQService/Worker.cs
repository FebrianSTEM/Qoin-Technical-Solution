// # define Publish
#define Consume
using APIConsumer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConnection _connection;
        private IModel _channel;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            InitRabbitMq();
        }

        private void InitRabbitMq()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("qtest1", durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            _logger.LogInformation("Consumer Start Running at: {time}", DateTimeOffset.Now);
#if Publish
                Publish(channel);
#else
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                HandleMessage(message);
            };
            _channel.BasicConsume("qtest1", true, consumer);

            return Task.CompletedTask;
#endif
        }
        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public void HandleMessage(string content)
        {
            Console.WriteLine($"Gotcha -- {content}");
            Test01Consumer.ConsumedAPITest01(content);
            Console.WriteLine($"Done Execute -- {content}");
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}