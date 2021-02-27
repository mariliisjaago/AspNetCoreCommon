using DbAccess_Library.Models;
using System.Threading.Tasks;

namespace DbAccess_Library.Contracts.Repos
{
    public interface IOrdersRepo
    {
        Task<int> CreateOrderAsync(OrderModel order);
        Task<OrderModel> GetById(int orderId);
        Task<int> UpdateOrderName(int orderId, string orderName);
        Task<int> Delete(int orderId);
    }
}
