using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Minesweeper
{
    public class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Load main page.
            MainPage = new NavigationPage(new AccountSignIn());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
