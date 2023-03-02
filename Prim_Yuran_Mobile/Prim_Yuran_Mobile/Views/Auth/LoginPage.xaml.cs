using Prim_Yuran_Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prim_Yuran_Mobile.Views.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public AuthService authService { get; set; } = new AuthService();

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var result = await authService.Login(PhoneOrEmailEntry.Text, PasswordEntry.Text);

            if (result == true)
            {
                PhoneOrEmailEntry.Text = string.Empty; PasswordEntry.Text = string.Empty;
                await DisplayAlert("Alert", "Successfully Login.", "Ok");
                await Shell.Current.GoToAsync($"//HomePage");
            }
            else
            {
                await DisplayAlert("Alert", "Login Failed.", "Ok");
            }
        }
    }
}