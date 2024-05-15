using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Share;
using System.Data;
using System.Text.Json;
using System.Text;

namespace WorkerServiceConsumer
{
    public class Worker : BackgroundService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly ILogger<Worker> _logger;
        private IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, RabbitMQClientService rabbitMQClientService, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _rabbitMQClientService = rabbitMQClientService;
            _serviceProvider = serviceProvider;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);
            consumer.Received += Consumer_Received;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(Encoding.UTF8.GetString(@event.Body.ToArray()));

            Console.WriteLine("\nNew Weather Forecast Notification!!!");
            Console.WriteLine($"{nameof(weatherForecast.Date)}:{weatherForecast.Date.ToString("f")} {nameof(weatherForecast.TemperatureF)}:{weatherForecast.TemperatureF} {nameof(weatherForecast.TemperatureC)}:{weatherForecast.TemperatureC} {nameof(weatherForecast.Summary)}:{weatherForecast.Summary}");

            _channel.BasicAck(@event.DeliveryTag, false);
        }


    }
}
