using Smart_bike_G3.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_bike_G3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Name());
            // MainPage = new NavigationPage(new ResultsVideo());

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
