﻿using KD.WPF.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KD.WPF.Client.APIClient.RestServices
{
    public class StockRestService : RestServiceBase
    {
        HttpClient client;
        public StockRestService(IHttpClientFactory httpClientFactory, IClientApplicationConfiguration configuration) : base(httpClientFactory, configuration)
        {
            client = GetClient();
        }
        public async Task<List<StockModel>> GetAllStocksAsync()
        {
            {
                HttpRequestMessage request = await PrepareRequestMessageAsync(HttpMethod.Get, string.Format("{0}/api/Stocks", serverAddress));
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<List<StockModel>>(await response.Content.ReadAsStringAsync());
                return payload;
            }
        }
        public async Task<StockModel> GetStockAsync(Guid stockId)
        {
            {
                HttpRequestMessage request = await PrepareRequestMessageAsync(HttpMethod.Get, string.Format("{0}/api/Stocks/{1}", serverAddress, stockId));
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<StockModel>(await response.Content.ReadAsStringAsync());
                return payload;
            }
        }
        public async Task<StockModel> CreateStockAsync(StockModel stock)
        {
            {
                var request = PrepareRequestMessageAsync(HttpMethod.Post, string.Format("{0}/api/Stocks", serverAddress)).Result;
                var jsonPayload = SerializeObject(stock);
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = client.SendAsync(request).Result;
                var payload = DeserializeObject<StockModel>(response.Content.ReadAsStringAsync().Result);
                return payload;
            }
        }
        public async Task<StockModel> UpdateStockAsync(Guid stockId, StockModel stock)
        {
            {
                var request = await PrepareRequestMessageAsync(HttpMethod.Put, string.Format("{0}/api/Stocks/{1}", serverAddress, stockId));
                var jsonPayload = SerializeObject(stock);
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<StockModel>(response.Content.ReadAsStringAsync().Result);
                return payload;
            }
        }
        public async Task DeleteStockAsync(Guid stockId)
        {
            {
                var request = await PrepareRequestMessageAsync(HttpMethod.Delete, string.Format("{0}/api/Stocks/{1}", serverAddress, stockId));
                var response = await client.SendAsync(request);
                await Task.CompletedTask;
            }
        }
    }
}
