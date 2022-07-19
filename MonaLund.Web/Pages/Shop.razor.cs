using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MonaLund.Web.Models;
using MonaLund.Web.Models.Contexts;

namespace MonaLund.Web.Pages
{
    public partial class Shop
    {
        [Inject]
        public MonaContext Context{ get; set; }
        [Parameter]
        public string SelectedCategory { get; set; }
        List<CategoryModel> _categories = new List<CategoryModel>();
        List<ProductModel> _products = new List<ProductModel>();
        List<string> _categoryFiltersApplied = new List<string>() { "all" };
        List<string> _subCategoryFiltersApplied = new List<string>() {  };
        private int itemCount = 0;
        private bool isSubcategoryToggled = true;

        protected override void OnInitialized()
        {
            _categories = _context.Categories.Include(c => c.SubCategories).ThenInclude(sc => sc.Products).ToList();

            if (string.IsNullOrWhiteSpace(SelectedCategory) == false)
            {
                CheckChangedCategory(SelectedCategory);
            } else
            {
                _products = _context.Products.ToList();
            }

            itemCount = _products.Count;
        }

        private bool IsCheckedCategory(string name)
        {
            
            return _categoryFiltersApplied.Contains(name);
        }

        private bool IsCheckedSubCategory(string name)
        {
            return _subCategoryFiltersApplied.Contains(name);
        }

        private void CheckChangedCategory(string name)
        {

            if (name == "all")
            {
                if (IsCheckedCategory(name)) // If all is already checked
                {
                    _categoryFiltersApplied.Clear();
                    _products.Clear(); // Need to move this
                }
                else // If re-toggled all, then load everything again
                {
                    _categoryFiltersApplied.Clear();
                    _categoryFiltersApplied.Add("all");
                    _products = Context.Products.ToList();
                }
            }
            else // If user did not select all
            {
                if (IsCheckedCategory(name)) // if already checked, then disable
                {
                    _categoryFiltersApplied.Remove(name);
                    _context.Categories.FirstOrDefault(c => c.Name == name).IsCategorySelected = true;
                    UpdateCategoryProducts();
                }
                else // If not checked then enable
                {
                    _categoryFiltersApplied.Clear();
                    _categoryFiltersApplied.Add(name);
                    foreach (CategoryModel category in _context.Categories)
                    {
                        category.IsCategorySelected = true;
                    }
                    _context.Categories.FirstOrDefault(c => c.Name == name).IsCategorySelected = false;

                    UpdateCategoryProducts();
                }
            }

        }

        private void CheckChangedSubCategory(string name)
        {
            if (IsCheckedSubCategory(name)) // if already checked, then disable
            {
                _subCategoryFiltersApplied.Remove(name);
                UpdateSubCategoryProducts(name);
            }
            else // If not checked then enable
            {
                _subCategoryFiltersApplied.Clear();
                _subCategoryFiltersApplied.Add(name);
                UpdateSubCategoryProducts(name);
            }
        }

        private void UpdateCategoryProducts()
        {
            //_products = Context.Products.Where(p => _categoryFiltersApplied.Contains(p.SubCategory.Name)).ToList();
            List<SubCategoryModel> subCategories = Context.SubCategories.Where(p => _categoryFiltersApplied.Contains(p.Category.Name)).ToList();
            _products.Clear();
            List<ProductModel> tempProductList = new List<ProductModel>();
            foreach (SubCategoryModel subCategory in subCategories)
            {
                tempProductList.AddRange(_context.Products.Where(p => p.SubCategory.Name == subCategory.Name).ToList());
            }

            _products = tempProductList;
        }

        private void UpdateSubCategoryProducts(string name)
        {
            _products = Context.Products.Where(p => p.SubCategory.Name == name).ToList();
            //List<ProductModel> tempProductList = new List<ProductModel>();
            //    tempProductList.AddRange(_context.Products.Where(p => p.SubCategory.Name == subCategory.Name).ToList());


            //_products = tempProductList;
        }
    }
}
