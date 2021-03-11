using DbAccess_Library.Contracts.Repos;
using DbAccess_Library.Contracts.Strategies;
using System.Threading.Tasks;

namespace DbAccess_Library.Strategies
{
    public class DeleteOrderStrategy : IDeleteOrderStrategy
    {
        private readonly IOrdersRepo _ordersRepo;

        public DeleteOrderStrategy(IOrdersRepo ordersRepo)
        {
            _ordersRepo = ordersRepo;
        }

        public Task Delete(int id)
        {
            var response = _ordersRepo.Delete(id);

            return response;
        }
    }
}
