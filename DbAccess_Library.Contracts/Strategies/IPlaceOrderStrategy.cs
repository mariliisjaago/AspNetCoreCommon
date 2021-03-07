using DbAccess_Library.Models;
using System.Threading.Tasks;

namespace DbAccess_Library.Contracts.Strategies
{
    public interface IPlaceOrderStrategy
    {
        Task<int> Place(OrderModel order);

        Task UpdateOrderName(int id, string orderName);
    }
}
