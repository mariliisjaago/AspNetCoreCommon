using Dapper;
using DbAccess_Library.Contracts.Db;
using DbAccess_Library.Contracts.Repos;
using DbAccess_Library.Db;
using DbAccess_Library.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DbAccess_Library.Repos
{
    public class SqlOrdersRepo : IOrdersRepo
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionStringData;

        public SqlOrdersRepo(IDataAccess dataAccess, ConnectionStringData connectionStringData)
        {
            _dataAccess = dataAccess;
            _connectionStringData = connectionStringData;
        }

        public async Task<int> CreateOrderAsync(OrderModel order)
        {
            string sql = "insert into dbo.Orders (OrderName, OrderDate, FoodId, Quantity, Total) " +
                "values (@OrderName, @OrderDate, @FoodId, @Quantity, @Total); " +
                "select SCOPE_IDENTITY();";

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("OrderName", order.OrderName);
            parameters.Add("OrderDate", order.OrderDate);
            parameters.Add("FoodId", order.FoodId);
            parameters.Add("Quantity", order.Quantity);
            parameters.Add("Total", order.Total);

            var newlyCreatedId = await _dataAccess.Save(sql, parameters, _connectionStringData.SqlConnectionName);

            return newlyCreatedId;
        }

        public async Task<OrderModel> GetById(int orderId)
        {
            string sql = "select [Id], [OrderName], [OrderDate], [FoodId], [Quantity], [Total] from dbo.Orders " +
                "where Id = @Id;";

            var orders = await _dataAccess.Load<OrderModel>(sql, new { Id = orderId }, _connectionStringData.SqlConnectionName);

            return orders.FirstOrDefault();
        }

        public Task<int> UpdateOrderName(int orderId, string orderName)
        {
            string sql = "update dbo.Orders " +
                "set OrderName = @OrderName " +
                "where Id = @Id;";

            return _dataAccess.Save(sql, new { OrderName = orderName, Id = orderId }, _connectionStringData.SqlConnectionName);
        }

        public Task<int> Delete(int orderId)
        {
            string sql = "delete from dbo.Orders where Id = @Id;";

            return _dataAccess.Save(sql, new { Id = orderId }, _connectionStringData.SqlConnectionName);
        }
    }
}
