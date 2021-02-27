using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbAccess_Library.Contracts.Db
{
    public interface IDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sqlStatement, U parameters, string connectionStringName);
        Task<int> SaveData<U>(string sqlStatement, U parameters, string connectionStringName);
    }
}