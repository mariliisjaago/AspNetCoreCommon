using DbAccess_Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbAccess_Library.Contracts.Repos
{
    public interface IFoodsRepo
    {
        Task<List<FoodModel>> GetAllFoods();
    }
}