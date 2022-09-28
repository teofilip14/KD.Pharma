using System.Net.Http;

namespace KD.WPF.Client.APIClient
{
    public class HttpClientFactory : IHttpClientFactory
    {
        static HttpClient client = new();

        public HttpClient GetHttpClient()
        {
            return client;
        }
    }
}
