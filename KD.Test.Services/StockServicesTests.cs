using KD.Core.Data;
using KD.Core.DomainModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using KD.Common.Model.Automapper;
using KD.Services.Stock;
using KD.Services.Product;
using KD.Common.Model.Models;
using Product = KD.Core.DomainModels.Product;

namespace KD.Test.Services
{
    public class StockServicesTests
    {
        private EFCoreRepository<Stock> stockRepository;
        private EFCoreRepository<Product> productRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            //Set up DbContext
            var options = new DbContextOptionsBuilder<PharmacyContext>();
            options.UseSqlServer("Server=.\\SQLEXPRESS;Database=Pharmacy;Trusted_Connection=True;");
            PharmacyContext _dbContext = new PharmacyContext(options.Options);

            //Set up Repos
            stockRepository = new(_dbContext);
            productRepository = new(_dbContext);

            //Set up Automapper
            AutoMapperConfiguration.Init();
            AutoMapperConfiguration.MapperConfiguration.AssertConfigurationIsValid();
        }

        [Test]
        public void GetStocksTest()
        {
            //arrange
            StockService service = GetService();

            //act
            var stocks = service.GetStocks();

            //assert
            Assert.That(stocks.Any());
        }

        [Test]
        public void CreateStockTest()
        {
            //arrange
            StockService service = GetService();
            Guid createdStockId = Guid.Empty;
            ProductService productservice = GetProductService();
            ProductModel product = CreateProductModel("Akadika", "Alinan", 10, "").ToModel();
            ProductModel createdProduct = productservice.CreateProduct(product);

            try
            {
                Stock stock = CreateStockModel(50, createdProduct.ProductId);

                //act
                StockModel createdStock = service.CreateStock(stock.ToModel());
                createdStockId = createdStock.StockId;

                //assert
                Assert.That(createdStock != null);
                Assert.That(createdStock?.Quantity == stock.Quantity);
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                service.DeleteStock(createdStockId);
            }

        }

        [Test]
        public void DeleteStockTest()
        {
            //arrange
            ProductService productservice = GetProductService();
            ProductModel product = CreateProductModel("Paracetamol", "Catena", 10, "").ToModel();
            ProductModel createdProduct = productservice.CreateProduct(product);

            StockService service = GetService();
            StockModel stock = CreateStockModel(20, createdProduct.ProductId).ToModel();
            StockModel createdStock = service.CreateStock(stock);

            //act
            service.DeleteStock(createdStock.StockId);

            //assert
            var deletedStock = service.GetStockById(createdStock.StockId);
            Assert.That(deletedStock == null);
        }

        [Test]
        public void UpdateStockTest()
        {
            ProductService productservice = GetProductService();
            ProductModel product = CreateProductModel("Paracetamol", "Catena", 10, "").ToModel();
            ProductModel createdProduct = productservice.CreateProduct(product);


            StockService service = GetService();
            StockModel stock = CreateStockModel(20, createdProduct.ProductId).ToModel();
            StockModel createdStock = service.CreateStock(stock);

            //act
            Setup();
            service = GetService();
            createdStock.Quantity = 20;
            service.UpdateStock(createdStock);

            //assert
            var updatedStock = service.GetStockById(createdStock.StockId);
            Assert.That(updatedStock != null);
        }

        private StockService GetService()
        {
            return new(stockRepository);
        }
        private ProductService GetProductService()
        {
            return new(productRepository, stockRepository);
        }
        private Stock CreateStockModel(int quantity, Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentException();
            }
            KD.Core.DomainModels.Stock stock = new()
            {
                ProductId = productId,
                Quantity = quantity,
            };
            return stock;
        }
        private Product CreateProductModel(string name, string producer, int price, string description = null)
        {
            KD.Core.DomainModels.Product product = new()
            {
                Name = name,
                Producer = producer,
                Price = price,
                Description = description,
            };


            return product;
        }
    }
}
