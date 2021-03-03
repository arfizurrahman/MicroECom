using MicroEcomWeb.ApiCollection.Infrastructure;
using MicroEcomWeb.ApiCollection.Interfaces;
using MicroEcomWeb.Models;
using MicroEcomWeb.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroEcomWeb.ApiCollection
{
    public class OrderApi : BaseHttpClientWithFactory, IOrderApi
    {
        private readonly IApiSettings _settings;

        public OrderApi(IHttpClientFactory factory, IApiSettings settings)
            : base(factory)
        {
            _settings = settings;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                           .SetPath(_settings.OrderPath)
                           .AddQueryString("username", userName)
                           .HttpMethod(HttpMethod.Get)
                           .GetHttpMessage();

            return await SendRequest<IEnumerable<OrderResponseModel>>(message);
        }
    }
}
