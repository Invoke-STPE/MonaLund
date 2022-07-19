using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonaLund.Web.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public ICollection<SubCategoryModel> SubCategories { get; set; }
        [NotMapped]
        public bool IsCategorySelected { get; set; } = true;
    }
}
