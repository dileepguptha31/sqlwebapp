using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webappwithsqldb.Models;
using webappwithsqldb.Services;

namespace webappwithsqldb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<Product> Products;
        public void OnGet()
        {
            Products = _productService.GetProducts().GetAwaiter().GetResult(); ;

        }
    }
}