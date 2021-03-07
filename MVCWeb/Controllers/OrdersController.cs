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
        private readonly IGetOrderStrategy _getOrderStrategy;
        private readonly IDeleteOrderStrategy _deleteOrderStrategy;

        public OrdersController(IFoodsRepo foodsRepo, IPlaceOrderStrategy placeOrderStrategy, IGetOrderStrategy getOrderStrategy,
            IDeleteOrderStrategy deleteOrderStrategy)
        {
            _foodsRepo = foodsRepo;
            _placeOrderStrategy = placeOrderStrategy;
            _getOrderStrategy = getOrderStrategy;
            _deleteOrderStrategy = deleteOrderStrategy;
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

            return RedirectToAction("Display", new { id });
        }

        public async Task<IActionResult> Display(int id)
        {
            OrderDisplayModel orderDisplay = new OrderDisplayModel();

            (OrderModel order, string foodTitle) data = await _getOrderStrategy.GetOrderAndFoodTitle(id);

            orderDisplay.Order = data.order;

            orderDisplay.ItemPurchased = data.foodTitle;

            return View(orderDisplay);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string orderName)
        {
            await _placeOrderStrategy.UpdateOrderName(id, orderName);

            return RedirectToAction("Display", new { id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            (OrderModel order, string foodTitle) orderInfo = await _getOrderStrategy.GetOrderAndFoodTitle(id);

            OrderDisplayModel orderDisplay = new OrderDisplayModel()
            {
                Order = orderInfo.order,
                ItemPurchased = orderInfo.foodTitle
            };

            return View(orderDisplay);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(OrderModel order)
        {
            _deleteOrderStrategy.Delete(order.Id);

            return RedirectToAction("Create");
        }
    }
}
