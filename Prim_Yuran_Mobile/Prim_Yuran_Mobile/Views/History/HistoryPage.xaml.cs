using Prim_Yuran_Mobile.Models;
using Prim_Yuran_Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace Prim_Yuran_Mobile.Views.History
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public YuranService yuranService { get; set; } = new YuranService();
        public HistoryPage()
        {
            InitializeComponent();

            TotalLabel.Text = $"Terdapat Bayaran Sejumlah : 0";
            _ = loadPickerAsync();
        }

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
            var selectedSchoolId = ((School) SchoolPicker.SelectedItem).id;

            var result = await yuranService.GetPayHistories(selectedSchoolId);

            if (result != null)
            {
                SejarahListView.ItemsSource = result;
                TotalLabel.Text = $"Terdapat Bayaran Sejumlah : {result.Count}";
            }
            else
                TotalLabel.Text = $"Terdapat Bayaran Sejumlah : 0";
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var selectReceiptId = ((PayHistory)(sender as Frame).BindingContext).id;

            var result = await yuranService.GetReceiptHtml(selectReceiptId);

            if (result != null) 
            {
                await this.Navigation.PushAsync(new ReceiptWebView(result));
            }
        }
    }
}