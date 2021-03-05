using DbAccess_Library.Contracts.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MVCWeb.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodsRepo _foodsRepo;

        public FoodController(IFoodsRepo foodsRepo)
        {
            _foodsRepo = foodsRepo;
        }

        public async Task<IActionResult> Index()
        {
            var foods = await _foodsRepo.GetAllFoods();

            return View(foods);
        }
    }
}
