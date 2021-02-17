using Acr.UserDialogs;
using Jewelry.ViewModels.Interfaces;
using Jewelry.Views;
using Models;
using Prism.Commands;
using Prism.Navigation;
using Service.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry.ViewModels
{
    public class LoginPageViewModel : ViewModelBase, IValidateUser
    {
        #region properties
        IGetUserService _getUserService { get; set; }
        public string UIUserName { get; set; }
        public string UIPassword { get; set; }
        public bool IsBusy { get; set; }
        #endregion

        #region Commands
        public DelegateCommand LoginCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        #endregion
        public LoginPageViewModel(INavigationService navigationService,
            IGetUserService getUserService) : base(navigationService)
        {
            _getUserService = getUserService;
            LoginCommand = new DelegateCommand(LoginCommandAsync);
            CancelCommand = new DelegateCommand(CancelCommandAsync);
        }

        /// <summary>
        /// Clear the entries
        /// </summary>
        private void CancelCommandAsync()
        {
            ResetTheCredentials();
        }
        /// <summary>
        /// Reset the values
        /// </summary>
        private void ResetTheCredentials()
        {
            UIUserName = string.Empty;
            UIPassword = string.Empty;
        }

        /// <summary>
        /// Login user depending upon the role
        /// </summary>
        private async void LoginCommandAsync()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;
                var userDetails = await ValidateUser(UIUserName, UIPassword);
                if (userDetails!=null)
                {
                    var navparams = new NavigationParameters();
                    navparams.Add("UserDetails", userDetails);
                    ResetTheCredentials();
                    await NavigationService.NavigateAsync(nameof(EstimationPage), navparams);
                }
            }
            catch (Exception ex)
            {
                await ShowAlert("Something went wrong", "Ok", string.Empty);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
            
        }

        /// <summary>
        /// Validate and get the user details
        /// </summary>
        /// <returns></returns>
        public async Task<User> ValidateUser(string userName,string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                await ShowAlert("Fields Cannot be Null", "Ok", string.Empty);
                return null;
            }
            using (UserDialogs.Instance.Loading())
            {
                var _userDetails = await _getUserService.GetUSerDetailsAsync();
                if (_userDetails.IsError)
                {
                    await ShowAlert("No Record Found", "Ok", string.Empty);
                    return null;
                }
                else
                {
                    var user = _userDetails.ResultObject.Where(x => x.UserName.Trim().ToUpper().Equals(userName.Trim().ToUpper())
                                                && x.Password.Equals(password)).FirstOrDefault();
                    if (user == null)
                    {
                        await ShowAlert("User not available or credentials are wrong", "Ok", string.Empty);
                        return null;
                    }
                    return user;
                }
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}
