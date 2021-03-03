using DbAccess_Library.Contracts.Strategies;
using DbAccess_Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace RazorPagesWeb.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly IGetOrder _getOrder;
        private readonly IDeleteOrder _deleteOrder;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public OrderModel Order { get; set; }
        public DeleteModel(IGetOrder getOrder, IDeleteOrder deleteOrder)
        {
            _getOrder = getOrder;
            _deleteOrder = deleteOrder;
        }

        public async Task<IActionResult> OnGet()
        {
            Order = await _getOrder.Get(Id);

            return Page();
        }

        public IActionResult OnPost()
        {
            _deleteOrder.Delete(Id);

            return RedirectToPage("./Create");
        }
    }
}
