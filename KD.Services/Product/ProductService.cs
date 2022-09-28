using KD.Common.Model.Automapper;
using KD.Common.Model.Models;
using KD.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KD.Services.Product
{
    public class ProductService : IProductService
    {
        #region Fields
        private readonly IRepository<Core.DomainModels.Product> productRepository;
        private readonly IRepository<Core.DomainModels.Stock> stockRepository;

        public ProductService(IRepository<Core.DomainModels.Product> productRepository, IRepository<Core.DomainModels.Stock> stockRepository)
        {
            this.productRepository = productRepository;
            this.stockRepository = stockRepository;
        }
        #endregion
        #region Methods

        public IEnumerable<ProductModel> GetProducts()
        {
            var products = productRepository.Table.Include(x=>x.Stock).Select(x => x.ToModel()).ToList();
            return products;
        }

        public ProductModel CreateProduct(ProductModel product)
        {
            if (product == null)
                return null;

            KD.Core.DomainModels.Product productEntity = product.ToEntity();
            productEntity.ProductId = Guid.Empty;
            productRepository.Insert(productEntity);

            ProductModel createdProduct = GetProductById(productEntity.ProductId);
            return createdProduct;
        }

        public ProductModel GetProductById(Guid productId)
        {
            var product = productRepository.Table.FirstOrDefault(s => s.ProductId == productId);
            return product.ToModel();
        }

        public ProductModel UpdateProduct(ProductModel product)
        {
            if (product == null) return null;
            var databaseEntity = productRepository.TableNoTracking.FirstOrDefault(s => s.ProductId == product.ProductId);
            if (databaseEntity == null)
                return null;

            productRepository.Update(product.ToEntity());
            return GetProductById(product.ProductId);
        }

        public void DeleteProduct(Guid productId)
        {
            var databaseEntity = productRepository.Table.FirstOrDefault(s => s.ProductId == productId);
            if (databaseEntity == null)
                return;

            productRepository.Delete(databaseEntity);
        }
        #endregion
    }
}
