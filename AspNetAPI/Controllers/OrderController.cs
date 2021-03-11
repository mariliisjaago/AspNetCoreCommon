using AspNetAPI.Models;
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
        private readonly IDeleteOrderStrategy _deleteOrderStrategy;
        private readonly IFoodsRepo _foodsRepo;

        public OrderController(IPlaceOrderStrategy placeOrderStrategy, IGetOrderStrategy getOrderStrategy, IDeleteOrderStrategy deleteOrderStrategy, IFoodsRepo foodsRepo)
        {
            _placeOrderStrategy = placeOrderStrategy;
            _getOrderStrategy = getOrderStrategy;
            _deleteOrderStrategy = deleteOrderStrategy;
            _foodsRepo = foodsRepo;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var order = await _getOrderStrategy.Get(id);

            if (order != null)
            {
                var itemPurchased = await _foodsRepo.GetById(order.FoodId);

                var output = new { Order = order, ItemPurchased = itemPurchased.Title };

                return Ok(output);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(OrderModel order)
        {
            var selectedFood = await _foodsRepo.GetById(order.FoodId);

            order.Total = selectedFood.Price * order.Quantity;

            int id = await _placeOrderStrategy.Place(order);

            return Ok(new { Id = id });
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] OrderUpdateModel data)
        {
            await _placeOrderStrategy.UpdateOrderName(data.Id, data.UpdatedName);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            await _deleteOrderStrategy.Delete(id);

            return Ok();
        }
    }
}
