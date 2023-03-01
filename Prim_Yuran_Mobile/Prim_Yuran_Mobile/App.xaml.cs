using Prim_Yuran_Mobile.Views.Auth;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prim_Yuran_Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            CheckIfLoggedIn();
        }

        public async void CheckIfLoggedIn()
        {
            if (!Application.Current.Properties.ContainsKey("user_id"))
            {
                await Shell.Current.GoToAsync($"//LoginPage");
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
