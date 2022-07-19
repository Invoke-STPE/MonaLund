namespace MonaLund.Web.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Facebook { get; set; }
        public string Dba { get; set; }
        public string Images { get; set; }
        public SubCategoryModel SubCategory { get; set; }
    }
}
