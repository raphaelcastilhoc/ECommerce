using RabbitMQ.Client;
using System;

namespace ECommerce.EventBusRabbitMQ
{
    public interface IRabbitMQConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
