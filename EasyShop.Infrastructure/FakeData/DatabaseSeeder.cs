//using Bogus;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;
//using EasyShop.Core.Entities;
//using EasyShop.Infrastructure.Data;
//using Microsoft.Extensions.Logging;

//public partial class DatabaseSeeder
//{
//    private readonly DBContext _context;

//    public DatabaseSeeder(DBContext context)
//    {
//        _context = context;
//    }

//    public void SeedTestData()
//    {
//        // Logging statement to indicate the start of the seeding process
//        Console.WriteLine("Seeding test data started.");

//        // Ensure the database is created (if not already)
//        _context.Database.EnsureCreated();

//        // Create an instance of Faker<Product> to generate test data for the Product entity
//        var productFaker = new Faker<Product>()
//            .RuleFor(p => p.BrandName, f => f.Company.CompanyName())
//            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
//            .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
//            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription());
//        // Add other rules for generating data

//        // Generate 10 instances of Product
//        var products = productFaker.Generate(10);

//        // Add the generated products to the context
//        _context.Products.AddRange(products);

//        // Create an instance of Faker<ProductImage> to generate test data for the ProductImage entity
//        var productImageFaker = new Faker<ProductImage>()
//            .RuleFor(pi => pi.Color, f => f.Commerce.Color())
//            .RuleFor(pi => pi.ImageUrl, f => f.Internet.Url());
//        // Add other rules for generating data

//        // Generate 10 instances of ProductImage
//        var productImages = productImageFaker.Generate(10);

//        // Add the generated product images to the context
//        _context.ProductImages.AddRange(productImages);

//        // Save the changes to the database
//        _context.SaveChanges();

//        Console.WriteLine("Seeding test data completed successfully.");
//    }

//}
