using KD.WPF.Client.Models;
using KD.WPF.Client.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KD.WPF.Client.APIClient.RestServices
{
    public class UserRestService : RestServiceBase
    {
        HttpClient client;
        public UserRestService(IHttpClientFactory httpClientFactory, IClientApplicationConfiguration configuration) : base (httpClientFactory, configuration)
        {
            client = GetClient();
        }
        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            {
                HttpRequestMessage request = await PrepareRequestMessageAsync(HttpMethod.Get, string.Format("{0}/api/Users", serverAddress));
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<List<UserModel>>(await response.Content.ReadAsStringAsync());
                return payload;
            }
        }

        public async Task<UserModel> GetUserAsync(Guid userId)
        {
            {
                HttpRequestMessage request = await PrepareRequestMessageAsync(HttpMethod.Get, string.Format("{0}/api/Users/{1}", serverAddress, userId));
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<UserModel>(await response.Content.ReadAsStringAsync());
                return payload;
            }
        }

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            {
                var request = PrepareRequestMessageAsync(HttpMethod.Post, string.Format("{0}/api/Users", serverAddress)).Result;
                var jsonPayload = SerializeObject(user);
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = client.SendAsync(request).Result;
                var payload = DeserializeObject<UserModel>(response.Content.ReadAsStringAsync().Result);
                return payload;
            }
        }

        public async Task<UserModel> UpdateUserAsync(Guid userId, UserModel user)
        {
            {
                var request = await PrepareRequestMessageAsync(HttpMethod.Put, string.Format("{0}/api/Users/{1}", serverAddress, userId));
                var jsonPayload = SerializeObject(user);
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                var payload = DeserializeObject<UserModel>(response.Content.ReadAsStringAsync().Result);
                return payload;
            }
        }

        public async Task<bool> ValidateUser(String username, String password)
        {
            var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
            var json = JsonConvert.SerializeObject(encoded);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(string.Format("{0}/api/ValidateUser/", serverAddress), content);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(result);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            {
                var request = await PrepareRequestMessageAsync(HttpMethod.Delete, string.Format("{0}/api/Users/{1}", serverAddress, userId));
                var response = await client.SendAsync(request);
                await Task.CompletedTask;
            }
        }
    }
}
