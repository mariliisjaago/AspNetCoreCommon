using DbAccess_Library.Contracts.Repos;
using DbAccess_Library.Contracts.Strategies;
using DbAccess_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPlaceOrderStrategy _placeOrderStrategy;
        private readonly IGetOrderStrategy _getOrderStrategy;
        private readonly IFoodsRepo _foodsRepo;

        public OrderController(IPlaceOrderStrategy placeOrderStrategy, IGetOrderStrategy getOrderStrategy, IFoodsRepo foodsRepo)
        {
            _placeOrderStrategy = placeOrderStrategy;
            _getOrderStrategy = getOrderStrategy;
            _foodsRepo = foodsRepo;
        }

        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post(OrderModel order)
        {
            var selectedFood = await _foodsRepo.GetById(order.FoodId);

            order.Total = selectedFood.Price * order.Quantity;

            int id = await _placeOrderStrategy.Place(order);

            return Ok(new { Id = id });
        }
    }
}
