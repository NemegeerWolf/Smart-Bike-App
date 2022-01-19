using Smart_bike_G3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Regular.ttf", Alias = "Rubik-regular")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Bold.ttf", Alias = "Rubik-Bold")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-SemiBold.ttf", Alias = "Rubik-SemiBold")]

namespace Smart_bike_G3.Views
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Name : ContentPage
    {
        public static string User;
        public static bool PublishName = false;

        public Name()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();
                BtnNext.Clicked += BtnNext_Clicked;
            } else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
            
        }

        private async void warning(string user)
        {
            
            bool check = await Repository.CheckUsernameAsync(user);
            if(check == true)
            {
                bool answer = await DisplayAlert("Deze naam is al in gebruik.", $"Ben jij {user}?", "Nee", "Ja");
                if (answer == false)
                {
                    Navigation.PushAsync(new VideoOrGame());
                }
                else
                {
                    await DisplayAlert("Kies een andere naam.", "", "OK");
                }
            }
            else
            {
                Navigation.PushAsync(new VideoOrGame());
            }
            
        }

        private void BtnNext_Clicked(object sender, EventArgs e)
        {
            if (entName.Text != null)
            {
                
                User = entName.Text;
                warning(User);
                //Navigation.PushAsync(new VideoOrGame());
                
            }
            else
            {
                lblValidation.IsVisible = true;
            }
               
        }

        private void checkPublish_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Console.WriteLine("checkbox changed");
            Console.WriteLine(checkPublish.IsChecked);
            PublishName = checkPublish.IsChecked;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //tabbed label checkbox
            if (checkPublish.IsChecked)
            {
                checkPublish.IsChecked = false;
            } else
            {
                checkPublish.IsChecked = true;
            }
        }
    }
}