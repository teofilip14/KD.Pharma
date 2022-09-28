using KD.Common.Model.Models;
using KD.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace KD.Web.API.Controllers
{
    [Authorize]
    [ApiController]    
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [AllowAnonymous]
        [Route("/api/Products")]
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            try
            {
                var products = productService.GetProducts();

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Products/{productId}")]
        [HttpGet]
        public ProductModel Get(Guid productId)
        {
            try
            {
                var product = productService.GetProductById(productId);

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Products/{productId}")]
        [HttpDelete]
        public void Delete(Guid productId)
        {
            try
            {
                productService.DeleteProduct(productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Products")]
        [HttpPost]
        public ProductModel Create([FromBody] ProductModel product)
        {
            try
            {
                ProductModel createdProduct = productService.CreateProduct(product);
                return createdProduct;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Products/{productId}")]
        [HttpPut]
        public ProductModel Update(Guid productId, [FromBody] JsonElement product)
        {
            try
            {
                var productModel = JsonSerializer.Deserialize<ProductModel>(product);
                ProductModel updatedProduct = productService.UpdateProduct(productModel);
                return updatedProduct;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}