using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebAssembly.Models
{
    public class OrderDisplayModel
    {
        public OrderModel Order { get; set; }
        public string ItemPurchased { get; set; }

    }
}
