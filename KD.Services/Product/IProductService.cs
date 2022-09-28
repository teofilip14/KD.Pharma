using KD.Common.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD.Services.Product
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetProducts();
        ProductModel GetProductById(Guid productId);
        void DeleteProduct(Guid productId);
        ProductModel CreateProduct(ProductModel product);
        ProductModel UpdateProduct(ProductModel product);
    }
}
