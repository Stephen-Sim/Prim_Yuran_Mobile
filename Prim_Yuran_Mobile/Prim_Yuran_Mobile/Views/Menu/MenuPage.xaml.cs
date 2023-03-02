using Prim_Yuran_Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prim_Yuran_Mobile.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
            authService = new AuthService();

            _ = loadData();
        }

        public async Task loadData()
        {
            var result = await authService.getUserInfo();

            if (result != null)
            {
                UsernameLabel.Text = result.name;
                TelnoLabel.Text = result.telno;
            }
        }

        public AuthService authService { get; set; }

        private async void ExitFrameTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Alert", "Adakah anda ingin log keluar aplikasi?", "Yes", "No");

            if (result)
            {
                Application.Current.Properties.Clear();
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }
    }
}