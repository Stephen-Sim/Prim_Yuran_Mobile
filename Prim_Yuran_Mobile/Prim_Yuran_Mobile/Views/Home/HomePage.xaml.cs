using Prim_Yuran_Mobile.Models;
using Prim_Yuran_Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prim_Yuran_Mobile.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public double TotalPay { get; set; } = 0.0d;
        public List<string> id { get; set; }
        public List<string> category { get; set; }

        public HomePage()
        {
            InitializeComponent();
            TotalLabel.Text = "Jumlah Bayaran : RM " + TotalPay.ToString("0.00");
            _ = loadPickerAsync();
        }

        public YuranService yuranService { get; set; } = new YuranService();
        public async Task loadPickerAsync()
        {
            var result = await yuranService.GetSchools();

            if (result != null)
            {
                SchoolPicker.ItemsSource = result;
                SchoolPicker.ItemDisplayBinding = new Binding("nama");
            }
        }
        
        private async void SchoolPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedSchoolId = ((School)SchoolPicker.SelectedItem).id;
            var result = await yuranService.GetFees(selectedSchoolId);

            id = new List<string>();
            category = new List<string>();

            TotalPay = 0.0d;
            TotalLabel.Text = "Jumlah Bayaran : RM " + TotalPay.ToString("0.00");

            if (result != null)
            {
                var list = result.GroupBy(x => x.category).ToList();
                FeesListView.ItemsSource = list;
            }
        }

        private async void PayButton_Clicked(object sender, EventArgs e)
        {
            var decision = await DisplayAlert("Alert", "Meneruskan Bayaran Sejumlah RM " + TotalPay.ToString("0.00"), "Ok", "Cancel");

            if (decision)
            {
                var result = await yuranService.PayYuranHtml(id, category);

                if (result != null)
                {
                    await this.Navigation.PushAsync(new PayYuranWebView(result));
                }
            }
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var fee = (Fee)(sender as CheckBox).BindingContext;

            if (fee.category == "Kategory A")
            {
                if (category.Contains(fee.id))
                {
                    category.Remove(fee.id);
                    TotalPay = TotalPay - double.Parse(fee.price.Substring(3));
                }
                else
                {
                    category.Add(fee.id);
                    TotalPay = TotalPay + double.Parse(fee.price.Substring(3));
                }
            }
            else
            {
                if (id.Contains(fee.id))
                {
                    id.Remove(fee.id);
                    TotalPay = TotalPay - double.Parse(fee.price.Substring(3));
                }
                else
                {
                    id.Add(fee.id);
                    TotalPay = TotalPay + double.Parse(fee.price.Substring(3));
                }
            }

            TotalLabel.Text = "Jumlah Bayaran : RM " + TotalPay.ToString("0.00");
        }
    }
}