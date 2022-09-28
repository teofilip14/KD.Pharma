using System.Net.Http;

namespace KD.WPF.Client.APIClient
{
    public interface IHttpClientFactory
    {
        HttpClient GetHttpClient();
    }
}
