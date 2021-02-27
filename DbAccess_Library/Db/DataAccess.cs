using Dapper;
using DbAccess_Library.Contracts.Db;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbAccess_Library.Db
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration _config;

        public DataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> Load<T>(string sqlStatement, object parameters, string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<T>(sqlStatement, parameters);

                return rows.ToList();
            }
        }

        public async Task<int> Save(string sqlStatement, object parameters, string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return await connection.ExecuteAsync(sqlStatement, parameters);
            }
        }
    }
}
