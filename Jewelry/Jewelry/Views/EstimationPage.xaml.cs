using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jewelry.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstimationPage : ContentPage
    {
        public EstimationPage()
        {
            InitializeComponent();
        }

        private void CustomEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(weight.Text))
            {
               weight.Text = "0";
            }
            if (string.IsNullOrWhiteSpace(price.Text))
            {
                price.Text = "0";
            }
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}