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
    public partial class PayYuranWebView : ContentPage
    {
        public PayYuranWebView(string HtmlContent)
        {
            InitializeComponent();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = HtmlContent;
            webpageview.Source = htmlSource;
        }

        private async void webpageview_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url == "https://prim.my/derma")
            {
                await DisplayAlert("Alert", "Transaksi Berjaya", "OK");
                await AppShell.Current.GoToAsync("..");

            }

            if (e.Url == "https://prim.my/derma?failed=1")
            {
                await DisplayAlert("Alert", "Transaksi Dibatalkan", "OK");
                await AppShell.Current.GoToAsync("..");
            }
        }
    }
}