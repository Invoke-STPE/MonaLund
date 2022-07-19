using System.ComponentModel.DataAnnotations.Schema;

namespace MonaLund.Web.Models
{
    public class SubCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryModel Category { get; set; }
        public ICollection<ProductModel> Products { get; set; }
       
    }
}
