using LoggingErrors.Models; 

namespace LoggingErrors.Services;

public interface IProductDataService
{
    // Helper methods 
    public bool CategoryExists(string categoryName);

    // Read Operations 
    public List<Product> GetAllProducts();

    public Product? GetProductById(int id);

    public List<Product> GetProductsByCategory(string categoryName);

}
