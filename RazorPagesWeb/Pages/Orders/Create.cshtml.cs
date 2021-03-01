using DbAccess_Library.Contracts.Repos;
using DbAccess_Library.Contracts.Strategies;
using DbAccess_Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorPagesWeb.Pages.Orders
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IFoodsRepo _foodsRepo;
        private readonly IOrdersRepo _ordersRepo;
        private readonly IPlaceOrder _placeOrder;

        public List<SelectListItem> FoodItems { get; set; }
        public OrderModel Order { get; set; }

        public CreateModel(IFoodsRepo foodsRepo, IPlaceOrder placeOrder)
        {
            _foodsRepo = foodsRepo;
            _placeOrder = placeOrder;
        }

        public async Task OnGet()
        {
            await PopulateFoodItems();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            int id = await _placeOrder.Place(Order);

            return RedirectToPage("./Display", new { Id = id });
        }

        private async Task PopulateFoodItems()
        {
            FoodItems = new List<SelectListItem>();

            var allFoods = await _foodsRepo.GetAllFoods();

            foreach (var food in allFoods)
            {
                AddToFoodItems(food);
            }
        }

        private void AddToFoodItems(FoodModel food)
        {
            FoodItems.Add(new SelectListItem
            {
                Value = food.Id.ToString(),
                Text = food.Title
            });
        }
    }
}
