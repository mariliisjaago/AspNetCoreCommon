using DbAccess_Library.Contracts.Db;
using DbAccess_Library.Contracts.Repos;
using DbAccess_Library.Db;
using DbAccess_Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DbAccess_Library.Repos
{
    public class SqlFoodsRepo : IFoodsRepo
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionStringData;

        public SqlFoodsRepo(IDataAccess dataAccess, ConnectionStringData connectionStringData)
        {
            _dataAccess = dataAccess;
            _connectionStringData = connectionStringData;
        }

        public Task<List<FoodModel>> GetAllFoods()
        {
            string sql = "select [Id], [Title], [Description], [Price] from dbo.Foods;";

            Task<List<FoodModel>> foods = _dataAccess.Load<FoodModel>(sql, new { }, _connectionStringData.SqlConnectionName);

            return foods;
        }

        public async Task<FoodModel> GetById(int foodId)
        {
            string sql = "select [Id], [Title], [Description], [Price] from dbo.Foods " +
                "where Id = @Id;";

            var foods = await _dataAccess.Load<FoodModel>(sql, new { Id = foodId }, _connectionStringData.SqlConnectionName);

            return foods.FirstOrDefault();
        }
    }
}
