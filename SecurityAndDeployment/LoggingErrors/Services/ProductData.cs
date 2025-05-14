using LoggingErrors.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection.Metadata;

namespace LoggingErrors.Services; 

public class ProductData
{
    private readonly ILogger<ProductData> _logger;

    public List<Product> Products; 

    public ProductData(ILogger<ProductData> logger)
    {
        _logger = logger;

        _logger.LogInformation("Load All Products In Memory");

        Products = new List<Product>()
        {
            new Product(1,"Chai", "Beverages"),
            new Product(2,"Chang", "Beverages"),
            new Product(3,"Guaraná Fantástica", "Beverages"),
            new Product(4,"Côte de Blaye", "Beverages"),

            new Product(5, "Queso Cabrales", "Dairy Products"), 
            new Product(6, "Queso Manchego La Pastora", "Dairy Products"), 
            new Product(7, "Gorgonzola Telino", "Dairy Products"), 
            new Product(8, "Mascarpone Fabioli", "Dairy Products"), 
            new Product(9, "Geitost", "Dairy Products"), 
            new Product(10, "Raclette Courdavault", "Dairy Products"), 

            new Product(11, "Uncle Bob's Organic Dried Pears", "Produce"), 
            new Product(12, "Tofu", "Produce"), 
            new Product(13, "Rössle Sauerkraut", "Produce"), 
            new Product(14, "Manjimup Dried Apples", "Produce"), 
            new Product(15, "Longlife Tofu", "Produce"),
            
            new Product(16, "Ikura", "Seafood"),
            new Product(17, "Konbu", "Seafood"),
            new Product(18, "Carnarvon Tigers", "Seafood"),
            new Product(19, "Nord-Ost Matjeshering", "Seafood"),


            new Product(20, "Apple MacBook Pro", "Electronics"),
            new Product(21, "Samsung Galaxy S24", "Electronics"),
            new Product(22, "JBL Flip 6 Bluetooth Speaker", "Electronics"),
            new Product(23, "Dell UltraSharp 27 Monitor", "Electronics")
        };

        if (!Products.Any())
        {
            _logger.LogWarning("Failed to Load Products"); 
        }
    }
}
