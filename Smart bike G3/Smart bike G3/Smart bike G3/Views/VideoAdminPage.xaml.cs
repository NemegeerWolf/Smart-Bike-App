using Newtonsoft.Json;
using Smart_bike_G3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
                NavigationPage.SetHasNavigationBar(this, false);
                firstTimefileSetup();
                setEntrys();
                //delete();
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }

        }

        private void setEntrys()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "videoUrls.txt");
            List<VideoSettings> settings = JsonConvert.DeserializeObject<List<VideoSettings>>(File.ReadAllText(fileName));
            List<string> urls = new List<string>();
            foreach (var i in settings)
            {
                urls.Add(i.vid.Url);
            }
            video1.Text = urls[0];
            video2.Text = urls[1];
            video3.Text = urls[2];
            video4.Text = urls[3];
        }

        private void changeBtn_Clicked(object sender, EventArgs e)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "videoUrls.txt");
            File.Delete(fileName);
            string jsonFront = "{\"video\":{\"url\":\"";
            string jsonMid = "\",\"audio\":";
            string jsonEnd = "}}";
            string vidUrls = $"[{jsonFront}{video1.Text}{jsonMid}{getNewVideoOptions(radio1)}{jsonEnd},{jsonFront}{video2.Text}{jsonMid}{getNewVideoOptions(radio2)}{jsonEnd},{jsonFront}{video3.Text}{jsonMid}{getNewVideoOptions(radio3)}{jsonEnd},{jsonFront}{video4.Text}{jsonMid}{getNewVideoOptions(radio3)}{jsonEnd}]";
            Debug.WriteLine(vidUrls);
            File.WriteAllText(fileName, vidUrls);
        }

        private int getNewVideoOptions(RadioButton xNameVid)
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









        private void firstTimefileSetup()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "videoUrls.txt");
            if (!File.Exists(fileName))
            {
                Debug.WriteLine("Creating url storage");
                string vidUrls = "[{\"video\":{\"url\":\"https://www.youtube.com/watch?v=07d2dXHYb94&t=1s}\",\"audio\":0}},{\"video\":{\"url\":\"https://www.youtube.com/watch?v=07d2dXHYb94&t=1s}\",\"audio\":0}},{\"video\":{\"url\":\"https://www.youtube.com/watch?v=07d2dXHYb94&t=1s}\",\"audio\":1}},{\"video\":{\"url\":\"https://www.youtube.com/watch?v=07d2dXHYb94&t=1s}\",\"audio\":1}}]";
                File.WriteAllText(fileName, vidUrls);
            }
        }
    }
}