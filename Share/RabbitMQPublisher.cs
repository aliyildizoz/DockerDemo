using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Share
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public void Publish(WeatherForecast weatherForecast)
        {
            var channel = _rabbitMQClientService.Connect();

            var bodyString = JsonSerializer.Serialize(weatherForecast);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange:RabbitMQClientService.ExchangeName, routingKey:RabbitMQClientService.RoutingWeatherForecast, basicProperties: properties, body: bodyByte);
        }
    }
}
