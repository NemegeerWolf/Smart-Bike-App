using System;
using TestBluethoot.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestBluethoot
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new MainPage());
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
