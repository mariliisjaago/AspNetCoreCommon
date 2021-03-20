using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerWeb.Models
{
    public class OrderUpdateModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Max is 20 characters")]
        [MinLength(3, ErrorMessage = "Min is 3 characters")]
        public string OrderName { get; set; }

    }
}
