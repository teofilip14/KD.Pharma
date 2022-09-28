using KD.WPF.Client.APIClient;
using KD.WPF.Client.APIClient.RestServices;
using KD.WPF.Client.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace KD.WPF.Client.Modules
{
    public class MainModules : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Product>("Product");
            containerRegistry.RegisterForNavigation<Stock>("Stock");
            containerRegistry.RegisterForNavigation<User>("User");
            containerRegistry.Register<IHttpClientFactory, HttpClientFactory>();
            containerRegistry.Register<IClientApplicationConfiguration, ApplicationConfiguration>();
            containerRegistry.Register<ProductRestService>();
            containerRegistry.Register<StockRestService>();
            containerRegistry.Register<UserRestService>();
        }
    }
}
