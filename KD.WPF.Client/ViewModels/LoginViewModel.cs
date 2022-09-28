using KD.WPF.Client.APIClient.RestServices;
using KD.WPF.Client.Services.Interfaces;
using KD.WPF.Client.Views;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Diagnostics;
using System.Windows;
using Unity;

namespace KD.WPF.Client.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        private PrismApplication _application;
        private readonly UserRestService _userRestService;
        private string _title = "Kronsoft Pharma";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private string username;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }
        public DelegateCommand LoginCommand { get; set; }

        public delegate void CloseWindowDelegate();
        public CloseWindowDelegate CloseWindow { get; set; }


        public LoginViewModel(IUnityContainer container, IRegionManager regionManager, UserRestService userRestService)
        {
            _regionManager = regionManager;
            _container = container;
            _userRestService = userRestService;

            LoginCommand = new DelegateCommand(OnLogin);
        }
        private async void OnLogin()
        {
            bool success = await _userRestService.ValidateUser(Username, Password);
            if (success)
            {
                Trace.WriteLine("Logging in");
                CloseWindow.Invoke();
            }
            else
                MessageBox.Show("Login invalid.");
        }
    }
}
