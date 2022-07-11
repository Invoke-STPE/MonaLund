using System.ComponentModel.DataAnnotations;

namespace MonaLund.Web.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
