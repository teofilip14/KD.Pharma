using KD.Common.Model.Automapper;
using KD.Common.Model.Models;
using KD.Core.Data;
using KD.Core.DomainModels;
using KD.Services.Product;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace KD.Test.Services
{
    public class ProductServicesTests
    {
        private EFCoreRepository<Product> productRepository;
        private EFCoreRepository<Stock> stockRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            //Set up DbContext
            var options = new DbContextOptionsBuilder<PharmacyContext>();
            options.UseSqlServer("Server=.\\SQLEXPRESS;Database=Pharmacy;Trusted_Connection=True;");
            PharmacyContext _dbContext = new PharmacyContext(options.Options);

            //Set up Repos
            productRepository = new(_dbContext);

            //Set up Automapper
            AutoMapperConfiguration.Init();
            AutoMapperConfiguration.MapperConfiguration.AssertConfigurationIsValid();
        }

        [Test]
        public void GetProductsTest()
        {
            //arrange
            ProductService service = GetService();

            //act
            var products = service.GetProducts();


            //assert
            Assert.That(products.Any());
        }
        [Test]
        public void CreateProductTest()
        {
            //arrange
            ProductService service = GetService();
            Guid createdProductId = Guid.Empty;

            Product product = CreateProductModel("Paracetamol", "Catena", 25, 10, "");

            //act
            ProductModel createdProduct = service.CreateProduct(product.ToModel());
            createdProductId = createdProduct.ProductId;

            //assert
            Assert.That(createdProduct != null);
            Assert.That(createdProduct?.Name == product.Name);
            Assert.That(createdProduct?.Description == product.Description);
            Assert.That(createdProduct?.Price == product.Price);
            Assert.That(createdProduct?.Producer == product.Producer);
        }

        [Test]
        public void DeleteProductTest()
        {
            //arrange
            ProductService service = GetService();
            ProductModel product = CreateProductModel("Paracetamol", "Catena", 25, 10, "").ToModel();
            ProductModel createdProduct = service.CreateProduct(product);

            //act
            service.DeleteProduct(createdProduct.ProductId);

            //assert
            var deletedProduct = service.GetProductById(createdProduct.ProductId);
            Assert.That(deletedProduct == null);
        }

        [Test]
        public void UpdateProductTest()
        {
            //arrange
            ProductService service = GetService();
            ProductModel product = CreateProductModel("Paracetamol", "Catena", 25, 10, "").ToModel();
            ProductModel createdProduct = service.CreateProduct(product);

            //act
            Setup();
            service = GetService();
            createdProduct.Name = "Name";
            service.UpdateProduct(createdProduct);

            //assert
            var updatedProduct = service.GetProductById(createdProduct.ProductId);
            Assert.That(updatedProduct != null);
        }

        private ProductService GetService()
        {
            return new(productRepository, stockRepository);
        }

        private Product CreateProductModel(string name, string producer, int price, int quantity, string description = null)
        {
            KD.Core.DomainModels.Product product = new()
            {
                Name = name,
                Description = description,
                Price = price,
                Producer = producer,
            };

            KD.Core.DomainModels.Stock stock = new()
            {
                ProductId = product.ProductId,
                Quantity = quantity,
            };
            product.Stock = stock;
            return product;
        }
    }
}
