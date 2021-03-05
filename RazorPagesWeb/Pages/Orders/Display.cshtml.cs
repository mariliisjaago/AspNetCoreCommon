using DbAccess_Library.Contracts.Repos;
using DbAccess_Library.Contracts.Strategies;
using DbAccess_Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace RazorPagesWeb.Pages.Orders
{
    public class DisplayModel : PageModel
    {
        private readonly IGetOrderStrategy _getOrderStrategy;
        private readonly IOrdersRepo _ordersRepo;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public OrderModel Order { get; set; }
        public string ItemPurchased { get; set; }

        [BindProperty]
        public UpdateOrderModel UpdateOrder { get; set; }

        public DisplayModel(IGetOrderStrategy getOrderStrategy, IOrdersRepo ordersRepo)
        {
            _getOrderStrategy = getOrderStrategy;
            _ordersRepo = ordersRepo;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                (OrderModel order, string foodTitle) fullOrderData = await _getOrderStrategy.GetOrderAndFoodTitle(Id);

                Order = fullOrderData.order;

                ItemPurchased = fullOrderData.foodTitle;

                return Page();
            }
            catch (Exception ex)
            {
                Order = null;

                return Page();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            await _ordersRepo.UpdateOrderName(UpdateOrder.Id, UpdateOrder.OrderName);

            return RedirectToPage("./Display", new { UpdateOrder.Id });
        }
    }
}
