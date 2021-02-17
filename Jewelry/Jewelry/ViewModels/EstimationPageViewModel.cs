using Acr.UserDialogs;
using Jewelry.Services;
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
using Xamarin.Forms;

namespace Jewelry.ViewModels
{
    public class EstimationPageViewModel : ViewModelBase, IPermissions
    {
        #region properties
        public string UserType { get; set; }
        public decimal UIDiscount { get; set; }
        public decimal GoldWeight { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal GoldPricePerGram { get; set; }
        public string PrintMessage { get; set; }
        ILocalStorage _localStorage { get; set; }
        #endregion

        #region commands
        public DelegateCommand CalculateCommand { get; private set; }
        public DelegateCommand PrintToScreenCommand { get; private set; }
        public DelegateCommand PrintToFileCommand { get; private set; }
        public DelegateCommand PrintToPaperCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        #endregion

        #region constructor
        public EstimationPageViewModel(INavigationService navigationService,
            ILocalStorage localStorage) : base(navigationService)
        {
            _localStorage = localStorage;
            CalculateCommand = new DelegateCommand(CalculateCommandAsync);
            PrintToScreenCommand = new DelegateCommand(PrintToScreenCommandAsync);
            PrintToFileCommand = new DelegateCommand(PrintToFileCommandAsync);
            PrintToPaperCommand = new DelegateCommand(PrintToPaperCommandAsync);
            CancelCommand = new DelegateCommand(CancelCommandAsync); 
        }
        #endregion

        /// <summary>
        /// Cancel To logout command.
        /// </summary>
        private async void CancelCommandAsync()
        {
            try
            {
                using(UserDialogs.Instance.Loading())
                {
                    var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
                    foreach (var page in existingPages)
                    {
                        if (page is LoginPage)
                            continue;
                        if (page is EstimationPage)
                            continue;
                        App.Current.MainPage.Navigation.RemovePage(page);
                    }
                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception)
            {
                await ShowAlert("Something went wrong", "Ok", string.Empty);
               
            }
            
        }

        /// <summary>
        /// Bluetooth printing.
        /// </summary>
        private async void PrintToPaperCommandAsync()
        {
            await ShowAlert("Printer Not Specified", "Ok", string.Empty);
        }

        /// <summary>
        /// Print to file and store in storage of device.
        /// </summary>
        private async void PrintToFileCommandAsync()
        {
            string displayPopUpMessage = string.Empty;
            try
            {
                await _localStorage.DeleteFile("JewelryLocal.txt");
                if (!string.IsNullOrEmpty(PrintMessage))
                {
                    //Depending on requirement can be saved in either of one location.
                    //1.Save in Local application folder can be retrieved using adb commands.
                    var write = await _localStorage.WriteTextAllAsync("JewelryLocal.Txt", PrintMessage);
                    if (write)
                    {
                        displayPopUpMessage = "File saved in application Folder";
                    }
                    else
                    {
                        displayPopUpMessage = "Something went wrong";
                    }

                    //2. Save in Download folder of internal storage.
                    var isSaved = await DependencyService.Get<IDownloadPath>().SaveFileAsyn(PrintMessage);
                    if (isSaved)
                    {
                        displayPopUpMessage = "Saved Successfully in InternalStorage/Download folder with name Jewelry Text.";
                    }
                    else
                    {
                        displayPopUpMessage = "Something went wrong";
                    }

                }
                else
                {
                    displayPopUpMessage = "Something went wrong or fields can't be empty";
                }
            }
            catch (Exception)
            {
                displayPopUpMessage = "Something went wrong";
            }
            finally
            {
                await ShowAlert(displayPopUpMessage, "Ok", string.Empty);
            }
        }

        /// <summary>
        /// Print to Screen in Pop up
        /// </summary>
        private async void PrintToScreenCommandAsync()
        {
            await ShowAlert(PrintMessage, "OK", string.Empty);
        }

        /// <summary>
        /// Calculate the Price
        /// </summary>
        private async void CalculateCommandAsync()
        {
            if (!(GoldWeight > 0 && GoldPricePerGram > 0))
            {
                TotalPrice = 0;
                await ShowAlert("Please give the Correct input values can't be 0", "OK", string.Empty);
                return;
            }
            TotalPrice = GoldWeight * GoldPricePerGram;
            if (UIDiscount>0)
            {
                var discount = TotalPrice * (UIDiscount / 100);
                TotalPrice = TotalPrice - discount;
            }
            PrintMessage = " " + GoldWeight.ToString() + " grams of gold. \n Rs" + GoldPricePerGram.ToString() +
                                                            " Gold PerGram. \n Total Price = Rs" + TotalPrice.ToString();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters?.ContainsKey("UserDetails") == true)
            {
                var currentUser = parameters["UserDetails"] as User;
                PermissionsAvailableForUser(currentUser);
            }
        }

        public void PermissionsAvailableForUser(User _currentUser)
        {
            if (_currentUser.Role.Trim() == RoleType.P.ToString())
            {
                UIDiscount = _currentUser.Discount;
                UserType = "Priveleged User";
            }
            else
            {
                UIDiscount = 0;
                UserType = "Normal User";
            }
        }
    }
}
