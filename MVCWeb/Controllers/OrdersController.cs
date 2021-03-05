using DbAccess_Library.Contracts.Repos;
using DbAccess_Library.Contracts.Strategies;
using DbAccess_Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWeb.Models;
using System.Threading.Tasks;

namespace MVCWeb.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IFoodsRepo _foodsRepo;
        private readonly IPlaceOrderStrategy _placeOrderStrategy;

        public OrdersController(IFoodsRepo foodsRepo, IPlaceOrderStrategy placeOrderStrategy)
        {
            _foodsRepo = foodsRepo;
            _placeOrderStrategy = placeOrderStrategy;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var foods = await _foodsRepo.GetAllFoods();

            OrderCreateModel model = new OrderCreateModel();

            foreach (var food in foods)
            {
                model.FoodItems.Add(new SelectListItem
                {
                    Value = food.Id.ToString(),
                    Text = food.Title
                });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateModel viewModel)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            var selectedFood = await _foodsRepo.GetById(viewModel.Order.FoodId);

            viewModel.Order.Total = viewModel.Order.Quantity * selectedFood.Price;

            int id = await _placeOrderStrategy.Place(viewModel.Order);

            return RedirectToAction("Create");
        }
    }
}
