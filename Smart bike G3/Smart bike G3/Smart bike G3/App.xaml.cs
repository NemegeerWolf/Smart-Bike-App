using Smart_bike_G3.Repositories;
using Smart_bike_G3.Views;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_bike_G3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            firstTimefileSetup();
            //CatchSleepmode();
            MainPage = new NavigationPage(new VideoOrGame());
        }

        private void firstTimefileSetup() //Makes file for videoUrls if non exist
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "videoUrls.txt");
            if (!File.Exists(fileName))
            {
                Debug.WriteLine("Creating url storage");
                string vidUrls = "[{\"video\":{\"url\":\"https://www.youtube.com/watch?v=07d2dXHYb94&t=1s}\",\"audio\":0}},{\"video\":{\"url\":\"https://www.youtube.com/watch?v=07d2dXHYb94&t=1s}\",\"audio\":0}},{\"video\":{\"url\":\"https://www.youtube.com/watch?v=07d2dXHYb94&t=1s}\",\"audio\":1}},{\"video\":{\"url\":\"https://www.youtube.com/watch?v=07d2dXHYb94&t=1s}\",\"audio\":1}}]";
                File.WriteAllText(fileName, vidUrls);
            }
        }

        public void CatchSleepmode()
        {
            Device.StartTimer(TimeSpan.FromMinutes(1), () =>
            {
                Console.WriteLine("Navigate back to Namepage");
                var navigation = MainPage.Navigation;
                navigation.PopToRootAsync(true); //clear stack history
                navigation.PushAsync(new VideoOrGame());
                return false;
            });
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            //MainPage = new NavigationPage(new Name()); 
        }

        protected override void OnResume()
        {
            //MainPage = new NavigationPage(new Name());

            var navigation = MainPage.Navigation;
            navigation.PopToRootAsync(true); //clear stack history
            navigation.PushAsync(new VideoOrGame());
        }
    }
}
