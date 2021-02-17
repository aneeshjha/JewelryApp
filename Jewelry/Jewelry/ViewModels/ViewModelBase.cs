using Acr.UserDialogs;
using Jewelry.ViewModels.Interfaces;
using Prism.Mvvm;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Threading.Tasks;

namespace Jewelry.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible, IAlert
    {
        #region properties
        public INavigationService NavigationService { get; set; }
        #endregion
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
        public void Destroy()
        {
            
        }

        public void Initialize(INavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }

        public async Task ShowAlert(string message, string okText, string cancelText)
        {
            await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Message = message,
                OkText = okText,
                CancelText = cancelText
            });
        }
    }
}
