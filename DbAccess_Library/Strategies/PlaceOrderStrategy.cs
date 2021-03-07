using DbAccess_Library.Contracts.Repos;
using DbAccess_Library.Contracts.Strategies;
using DbAccess_Library.Models;
using System.Threading.Tasks;

namespace DbAccess_Library.Strategies
{
    public class PlaceOrderStrategy : IPlaceOrderStrategy
    {
        private readonly IFoodsRepo _foodsRepo;
        private readonly IOrdersRepo _ordersRepo;

        public PlaceOrderStrategy(IFoodsRepo foodsRepo, IOrdersRepo ordersRepo)
        {
            _foodsRepo = foodsRepo;
            _ordersRepo = ordersRepo;
        }

        public IFoodsRepo FoodsRepo { get; }
        public IOrdersRepo OrdersRepo { get; }

        public async Task<int> Place(OrderModel order)
        {
            var selectedFood = await _foodsRepo.GetById(order.FoodId);

            order.Total = CalculateTotal(selectedFood, order);

            int orderId = await _ordersRepo.CreateOrderAsync(order);

            return orderId;
        }

        public Task UpdateOrderName(int id, string orderName)
        {
            return _ordersRepo.UpdateOrderName(id, orderName);
        }

        private decimal CalculateTotal(FoodModel selectedFood, OrderModel order)
        {
            return selectedFood.Price * order.Quantity;
        }
    }
}
