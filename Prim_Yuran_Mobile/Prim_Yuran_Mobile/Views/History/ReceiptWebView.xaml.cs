using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prim_Yuran_Mobile.Views.History
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceiptWebView : ContentPage
    {
        public ReceiptWebView(string HtmlContent)
        {
            InitializeComponent();

            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = HtmlContent;
            webpageview.Source = htmlSource;
        }
    }
}