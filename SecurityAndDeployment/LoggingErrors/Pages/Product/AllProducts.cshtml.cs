using LoggingErrors.Models;
using LoggingErrors.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoggingErrors.Pages.Product
{
    public class AllProductsModel : PageModel
    {
        private readonly ILogger<AllProductsModel> _logger;  

        private IProductDataService service; 

        public AllProductsModel(IProductDataService service, ILogger<AllProductsModel> logger)
        {
            this.service = service;
            _logger = logger;
        }


        // Data shred with view 
        public List<Models.Product> ProductList { get; private set; } = new List<Models.Product>();

        public IActionResult OnGet()
        {
            ProductList = service.GetAllProducts();
            _logger.LogInformation("Loaded {ProductCount} Products", ProductList.Count); 
            return Page();
        }
    }
}
