using DbAccess_Library.Contracts.Repos;
using DbAccess_Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodsRepo _foodsRepo;

        public FoodController(IFoodsRepo foodsRepo)
        {
            _foodsRepo = foodsRepo;
        }

        [HttpGet]
        public async Task<List<FoodModel>> Get()
        {
            var foods = await _foodsRepo.GetAllFoods();

            return foods;
        }
    }
}
