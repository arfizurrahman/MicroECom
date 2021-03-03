using System;
using System.Threading.Tasks;
using MicroEcomWeb.ApiCollection.Interfaces;
using MicroEcomWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroEcomWeb
{
    public class CheckOutModel : PageModel
    {
        private readonly IBasketApi _basketApi;
        private readonly IOrderApi _orderApi;

        public CheckOutModel(IBasketApi basketApi, IOrderApi orderApi)
        {
            _basketApi = basketApi ?? throw new ArgumentNullException(nameof(basketApi));
            _orderApi = orderApi ?? throw new ArgumentNullException(nameof(orderApi));
        }

        [BindProperty]
        public BasketCheckoutModel Order { get; set; }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = "swn";
            Cart = await _basketApi.GetBasket(userName);

            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            var userName = "swn";
            Cart = await _basketApi.GetBasket(userName);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.UserName = userName;
            Order.TotalPrice = Cart.TotalPrice;

            await _basketApi.CheckoutBasket(Order);

            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
    }
}