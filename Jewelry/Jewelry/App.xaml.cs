using Jewelry.ViewModels;
using Jewelry.Views;
using Prism;
using Prism.Ioc;
using Service.OfflineServices;
using Service.ServiceInterface;
using System;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jewelry
{
    public partial class App 
    {
        public static string abc { get; set; }
        public App(IPlatformInitializer initializer) 
            : base(initializer)
        { 

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.Register<IGetUserService, GetUserService>();
            containerRegistry.Register<ILocalStorage, LocalStorage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<EstimationPage, EstimationPageViewModel>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }
    }
}
