using KD.WPF.Client.APIClient;
using KD.WPF.Client.APIClient.RestServices;
using KD.WPF.Client.Modules;
using KD.WPF.Client.Services.Interfaces;
using KD.WPF.Client.ViewModels;
using KD.WPF.Client.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using System.Windows;

namespace KD.WPF.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {


            containerRegistry.RegisterForNavigation<Product>("Product");
            containerRegistry.RegisterForNavigation<Stock>("Stock");
            containerRegistry.RegisterForNavigation<User>("User");



            containerRegistry.Register<IHttpClientFactory, HttpClientFactory>();
            containerRegistry.Register<IClientApplicationConfiguration, ApplicationConfiguration>();

            containerRegistry.Register<ProductViewModel>();
            containerRegistry.Register<StockViewModel>();
            containerRegistry.Register<UserViewModel>();

            ViewModelLocationProvider.Register<Product, ProductViewModel>();
            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();



        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<MainModules>();
        }


        protected override void OnInitialized()
        {
            var login = Container.Resolve<Login>();
            var result = login.ShowDialog();

            if (result.Value)
                base.OnInitialized();


        }
    }
}
