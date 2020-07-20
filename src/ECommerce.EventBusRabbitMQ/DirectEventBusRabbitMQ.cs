using ECommerce.EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EventBusRabbitMQ
{
    public class DirectEventBusRabbitMQ : IEventBus, IDisposable
    {
        private readonly IRabbitMQConnection _connection;
        private readonly EventBusRabbitMQSettings _settings;
        private readonly ILogger<DirectEventBusRabbitMQ> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly int _retryCount;
        private readonly Policy _policy;

        private bool _disposed;

        private IModel _consumerChannel;
        private IModel _publisherChannel;

        public DirectEventBusRabbitMQ(IRabbitMQConnection connection,
            IOptions<EventBusRabbitMQSettings> options,
            ILogger<DirectEventBusRabbitMQ> logger,
            IServiceProvider serviceProvider)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = options.Value;
            _serviceProvider = serviceProvider;
            _consumerChannel = CreateChannel();
            _publisherChannel = CreateChannel();
            _retryCount = 5;

            _policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                        _logger.LogWarning(ex, "Could not publish event after {Timeout}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                    });
        }

        private IModel CreateChannel()
        {
            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            _logger.LogTrace("Creating RabbitMQ channel");

            var channel = _connection.CreateModel();
            channel.ExchangeDeclare(exchange: _settings.ExchangeName, type: "direct");

            return channel;
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            var eventName = @event.GetType().Name;

            _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);

            _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);

            _publisherChannel.ExchangeDeclare(exchange: _settings.ExchangeName, type: "direct");

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            _policy.Execute(() =>
            {
                var properties = _publisherChannel.CreateBasicProperties();
                properties.DeliveryMode = 2;

                _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);

                _publisherChannel.BasicPublish(
                    exchange: _settings.ExchangeName,
                    routingKey: eventName,
                    basicProperties: properties,
                    body: body);
            });
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;

            _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).Name);

            _consumerChannel.QueueBind(queue: eventName,
                                      exchange: _settings.ExchangeName,
                                      routingKey: eventName);

            _logger.LogTrace("Starting RabbitMQ basic consume");

            StartBasicConsume<T, TH>(eventName);
        }

        private void StartBasicConsume<T, TH>(string queueName) where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            _logger.LogTrace("Starting RabbitMQ basic consume");

            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);
            consumer.Received += Consumer_Received<T, TH>;

            _consumerChannel.BasicConsume(
                queue: queueName,
                autoAck: false,
                consumer: consumer);
        }

        private async Task Consumer_Received<T, TH>(object sender, BasicDeliverEventArgs eventArgs)
            where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

            try
            {
                await ProcessEvent<T, TH>(eventName, message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "----- ERROR Processing message \"{Message}\"", message);
            }

            _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }

        private async Task ProcessEvent<T, TH>(string eventName, string message)
            where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);

            using (var scope = _serviceProvider.CreateScope())
            {
                var integrationEvent = JsonConvert.DeserializeObject<T>(message);
                var enventHandler = scope.ServiceProvider.GetRequiredService<TH>();

                await enventHandler.Handle(integrationEvent);
            }

            _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _consumerChannel.Dispose();
                _publisherChannel.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
