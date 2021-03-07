using DbAccess_Library.Models;

namespace MVCWeb.Models
{
    public class OrderDisplayModel
    {
        public OrderModel Order { get; set; }

        public string ItemPurchased { get; set; }
    }
}
