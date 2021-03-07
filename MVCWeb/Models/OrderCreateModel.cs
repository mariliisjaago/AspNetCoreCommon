using DbAccess_Library.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MVCWeb.Models
{
    public class OrderCreateModel
    {
        public OrderModel Order { get; set; } = new OrderModel();

        public List<SelectListItem> FoodItems { get; set; } = new List<SelectListItem>();
    }
}
