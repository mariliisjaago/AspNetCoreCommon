using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebAssembly.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Max is 20 characters")]
        [MinLength(3, ErrorMessage = "Min is 3 characters")]
        [DisplayName("Name for the order")]
        public string OrderName { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        [Required]
        [DisplayName("Meal")]
        [Range(1, int.MaxValue, ErrorMessage = "Must be positive number")]
        public int FoodId { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "Can order up to 10 items")]
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
