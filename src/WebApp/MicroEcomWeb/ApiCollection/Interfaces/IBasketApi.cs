using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroEcomWeb.ApiCollection.Interfaces
{
    public interface IBasketApi
    {
        Task<BasketModel> GetBasket(string userName);
        Task<BasketModel> GetBasket(BasketModel model);
        Task CheckoutBasket(BasketCheckoutModel model);
    }
}
