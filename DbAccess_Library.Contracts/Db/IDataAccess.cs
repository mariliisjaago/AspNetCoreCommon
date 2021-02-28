using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbAccess_Library.Contracts.Db
{
    public interface IDataAccess
    {
        Task<List<T>> Load<T>(string sqlStatement, object parameters, string connectionStringName);
        Task<int> InsertAndGetId(string sqlStatement, object parameters, string connectionStringName);
        Task<int> Save(string sqlStatement, object parameters, string connectionStringName);
    }
}