using System.ComponentModel.DataAnnotations;

namespace BlazorWebAssembly.Models
{
    public class OrderUpdateModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Max is 20 characters")]
        [MinLength(3, ErrorMessage = "Min is 3 characters")]
        public string UpdatedName { get; set; }
    }
}
