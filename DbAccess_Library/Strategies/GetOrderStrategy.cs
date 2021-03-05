using DbAccess_Library.Contracts.Repos;
using DbAccess_Library.Contracts.Strategies;
using DbAccess_Library.Models;
using System.Threading.Tasks;

namespace DbAccess_Library.Strategies
{
    public class GetOrderStrategy : IGetOrderStrategy
    {
        private readonly IOrdersRepo _ordersRepo;
        private readonly IFoodsRepo _foodsRepo;

        public GetOrderStrategy(IOrdersRepo ordersRepo, IFoodsRepo foodsRepo)
        {
            _ordersRepo = ordersRepo;
            _foodsRepo = foodsRepo;
        }

        public async Task<(OrderModel Order, string FoodTitle)> GetOrderAndFoodTitle(int id)
        {
            OrderModel order = await _ordersRepo.GetById(id);

            var food = await _foodsRepo.GetById(order.FoodId);

            string foodTitle = food.Title;

            return (order, foodTitle);
        }

        public Task<OrderModel> Get(int id)
        {
            return _ordersRepo.GetById(id);
        }
    }
}
