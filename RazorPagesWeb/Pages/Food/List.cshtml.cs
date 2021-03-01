using DbAccess_Library.Contracts.Repos;
using DbAccess_Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorPagesWeb.Pages.Food
{
    public class ListModel : PageModel
    {
        private readonly IFoodsRepo _foodsRepo;

        public List<FoodModel> Foods { get; set; }
        public ListModel(IFoodsRepo foodsRepo)
        {
            _foodsRepo = foodsRepo;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Foods = await _foodsRepo.GetAllFoods();

            return Page();
        }
    }
}
