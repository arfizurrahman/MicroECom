using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using MediatR;
using Newtonsoft.Json;
using Ordering.Application.Commands;
using Ordering.Core.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Ordering.API.RabbitMQ
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IOrderRepository orderRepository;

        public EventBusRabbitMQConsumer(IRabbitMQConnection connection, IMediator mediator, IMapper mapper, IOrderRepository orderRepository)
        {

        }

        public void Consume()
        {
            var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: EventBusConstants.BasketCheckoutQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += RecievedEvent;

            channel.BasicConsume(queue: EventBusConstants.BasketCheckoutQueue, autoAck: false, consumer: consumer, noLocal: false, exclusive: false, arguments: null);
        }

        private async void RecievedEvent(object sender, BasicDeliverEventArgs e)
        {
            if(e.RoutingKey == EventBusConstants.BasketCheckoutQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var basicCheckoutEvent = JsonConvert.DeserializeObject<BasketCheckoutEvent>(message);

                var command = _mapper.Map<CheckoutOrderCommand>(basicCheckoutEvent);

                await _mediator.Send(command);
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}
