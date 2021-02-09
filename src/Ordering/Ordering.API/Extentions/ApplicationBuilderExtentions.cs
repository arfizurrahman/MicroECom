using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Ordering.API.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Extentions
{
    public static class ApplicationBuilderExtentions
    {
        public static EventBusRabbitMQConsumer Listener { get; set; }

        public static IApplicationBuilder UseRabbitMQListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusRabbitMQConsumer>();
            var lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            lifetime.ApplicationStarted.Register(OnStarted);
            lifetime.ApplicationStopping.Register(OnStopping);

            return app;
        }

        public static void OnStarted()
        {
            Listener.Consume();
        }

        public static void OnStopping()
        {
            Listener.Disconnect();
        }
    }
}
