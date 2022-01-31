using Newtonsoft.Json;
using Smart_bike_G3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Regular.ttf", Alias = "Rubik-Regular")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Bold.ttf", Alias = "Rubik-Bold")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-SemiBold.ttf", Alias = "Rubik-SemiBold")]


namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoAdminPage : ContentPage
    {
        public VideoAdminPage()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();

                //prevent sleepmode
                App app = new App();
                app.ToggleScreenLock(true);

                NavigationPage.SetHasNavigationBar(this, false);
                //SetEntrysAndButtons();
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += AbsLayBack_Tabbed;
                AbsLayBack.GestureRecognizers.Add(tapGestureRecognizer);
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
        }

        private void AbsLayBack_Tabbed(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        /*private void SetEntrysAndButtons()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "videoUrls.txt");
            List<VideoSettings> settings = JsonConvert.DeserializeObject<List<VideoSettings>>(File.ReadAllText(fileName));
            List<string> urls = new List<string>();
            List<int> buttns = new List<int>();
            foreach (var i in settings)
            {
                urls.Add(i.vid.Url);
                buttns.Add(i.vid.Audio);
            }
            video1.Text = urls[0];
            video2.Text = urls[1];
            video3.Text = urls[2];
            video4.Text = urls[3];
            SetRadio(buttns[0], radio10, radio11);
            SetRadio(buttns[1], radio20, radio21);
            SetRadio(buttns[2], radio30, radio31);
            SetRadio(buttns[3], radio40, radio41);
        }*/

        private void SetRadio(int val, RadioButton name, RadioButton name2)
        {
            if (val == 0)
            {
                name.IsChecked = true;
            }
            else
            {
                name2.IsChecked = true;
            }
        }

        private void ChangeBtn_Clicked(object sender, EventArgs e)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "videoUrls.txt");
            File.Delete(fileName);
            string jsonFront = "{\"video\":{\"url\":\"";
            string jsonMid = "\",\"audio\":";
            string jsonEnd = "}}";
            string vidUrls = $"[{jsonFront}{video1.Text}{jsonMid}{GetRadioOptions(radio10)}{jsonEnd},{jsonFront}{video2.Text}{jsonMid}{GetRadioOptions(radio20)}{jsonEnd},{jsonFront}{video3.Text}{jsonMid}{GetRadioOptions(radio30)}{jsonEnd},{jsonFront}{video4.Text}{jsonMid}{GetRadioOptions(radio40)}{jsonEnd}]";
            
            File.WriteAllText(fileName, vidUrls);
            Navigation.PushAsync(new ChooseVideo());
        }

        private int GetRadioOptions(RadioButton xNameVid)
        {
            if (xNameVid.IsChecked)
            {
                return 0;
            }
            else 
            {
                return 1;
            }
        }
    }
}