using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Share
{
    public class RabbitMQClientService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMQClientService> _logger;

        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "WeatherForecastExchange";
        public static string RoutingWeatherForecast = "weather-forecast-route";
        public static string QueueName = "weather-forecast-queue";

        public RabbitMQClientService(ConnectionFactory connectionFactory, ILogger<RabbitMQClientService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public IModel Connect()
        {
            this._connection = _connectionFactory.CreateConnection();

            if (_channel is { IsOpen: true })
            {
                return this._channel;
            }

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, type: ExchangeType.Direct, true, false);

            _channel.QueueDeclare(QueueName, true, false, false, null);

            _channel.QueueBind(exchange: ExchangeName, queue: QueueName, routingKey: RoutingWeatherForecast);
            _logger.LogInformation("Connected RabbitMQ successfuly...");

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();

            _connection?.Close();
            _connection?.Dispose();

            _logger.LogInformation("Lost connection with RabbitMQ...");
        }
    }
}
