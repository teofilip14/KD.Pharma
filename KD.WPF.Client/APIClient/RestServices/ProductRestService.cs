using KD.WPF.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KD.WPF.Client.APIClient.RestServices
{
    public class ProductRestService : RestServiceBase
    {
        HttpClient client;
        public ProductRestService(IHttpClientFactory httpClientFactory, IClientApplicationConfiguration configuration) : base(httpClientFactory, configuration)
        {
            client = GetClient();
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            {
                HttpRequestMessage request = await PrepareRequestMessageAsync(HttpMethod.Get, string.Format("{0}/api/Products", serverAddress));
                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode.ToString());
                }
                var payload = DeserializeObject<List<ProductModel>>(await response.Content.ReadAsStringAsync());
                return payload;
            }
        }

        public async Task<ProductModel> GetProductAsync(Guid productId)
        {
            {
                HttpRequestMessage request = await PrepareRequestMessageAsync(HttpMethod.Get, string.Format("{0}/api/Products/{1}", serverAddress, productId));
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<ProductModel>(await response.Content.ReadAsStringAsync());
                return payload;
            }
        }

        public async Task<ProductModel> CreateProductAsync(ProductModel product)
        {
            {
                var request = PrepareRequestMessageAsync(HttpMethod.Post, string.Format("{0}/api/Products", serverAddress)).Result;
                var jsonPayload = SerializeObject(product);
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = client.SendAsync(request).Result;
                var payload = DeserializeObject<ProductModel>(response.Content.ReadAsStringAsync().Result);
                return payload;
            }
        }

        public async Task<ProductModel> UpdateProductAsync(Guid productId, ProductModel product)
        {
            {
                var request = await PrepareRequestMessageAsync(HttpMethod.Put, string.Format("{0}/api/Products/{1}", serverAddress, productId));
                var jsonPayload = SerializeObject(product);
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<ProductModel>(response.Content.ReadAsStringAsync().Result);
                return payload;
            }
        }
        public async Task DeleteProductAsync(Guid productId)
        {
            {
                var request = await PrepareRequestMessageAsync(HttpMethod.Delete, string.Format("{0}/api/Products/{1}", serverAddress, productId));
                var response = await client.SendAsync(request);
                await Task.CompletedTask;
            }
        }
    }
}