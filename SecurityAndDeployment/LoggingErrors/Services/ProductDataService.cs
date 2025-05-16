using LoggingErrors.Models;

namespace LoggingErrors.Services;

public class ProductDataService : IProductDataService
{
    private List<Product> list; 
    private readonly ILogger<ProductDataService> _logger;   

    public ProductDataService(ProductData service, ILogger<ProductDataService> logger)
    {
        list = service.Products;
        _logger = logger;
    }

    public bool CategoryExists(string categoryName)
    {
        var query =  list.Any(p => p.Category == categoryName);

        return query;
    }

    public List<Product> GetAllProducts()
    {
        var result = list;

        _logger.LogInformation("Get Product List"); 

        if (!result.Any())
        {
            _logger.LogWarning("Product List is Empty"); 
        }

        return result;
    }

    public Product? GetProductById(int id)
    {
        var query = list.FirstOrDefault(p  => p.Id == id);

        _logger.LogInformation("Find Product with ID {ProductID}", id);
        
        if (query is null)
        {
            _logger.LogWarning("Product with ID {ProudctID} does not exists", id); 
        }

        return query;
    }

    public List<Product> GetProductsByCategory(string categoryName)
    {
        var query = list.Where(p => categoryName == p.Category).ToList();

        _logger.LogInformation("Find All Products with Category Name {CategoryName}", categoryName);

        if (query is null)
        {
            _logger.LogWarning("Category with Name {CategoryName} does not exists", categoryName);
            return new List<Product>();
        }

        return query;
    }
}
