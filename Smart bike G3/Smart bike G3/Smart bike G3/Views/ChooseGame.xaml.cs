using Quick.Xamarin.BLE.Abstractions;
using Smart_bike_G3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Regular.ttf", Alias = "Rubik-Regular")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-Bold.ttf", Alias = "Rubik-Bold")]
[assembly: ExportFont(@"Smart_bike_G3.Fonts.Rubik-SemiBold.ttf", Alias = "Rubik-SemiBold")]

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseGame : ContentPage
    {

        public static int gameId;

        public ChooseGame()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();

                //prevent sleepmode
                
                DeviceDisplay.KeepScreenOn = false;

                Pictures();
                AddEvents();
                imgHelp.Clicked += ImgHelp_Clicked;
                btnOk.Clicked += BtnOk_Clicked;
                
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
            
        }

        //protected override void OnAppearing()
        //{

        //    if (Bluetooth.BleStatus != AdapterConnectStatus.Connected)
        //    {
        //        Navigation.PushAsync(new NoSensorPage());
        //    }
        //    base.OnAppearing();
        //}

        private void BtnOk_Clicked(object sender, EventArgs e)
        {
            GridHelp.IsVisible = false;
            GridHelpBackGround.IsVisible = false;
        }

        private void ImgHelp_Clicked(object sender, EventArgs e)
        {
            GridHelp.IsVisible = true;
            GridHelpBackGround.IsVisible = true;
        }

        private void Pictures()
        {
            ImgBackground.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Background.png");
            Img123Piano.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Light.png");
            ImgBalance.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Bike.png");
            ImgOverloop.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Glass.png");
            imgHelp.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.help.png");
        }

       

        private void AddEvents()
        {
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += AbsLayBack_Tabbed;
            AbsLayBack.GestureRecognizers.Add(tapGestureRecognizer);
            

            TapGestureRecognizer tapGestureRecognizer2 = new TapGestureRecognizer();
            tapGestureRecognizer2.Tapped += AbsLay123piano_Tabbed;
            AbsLay123piano.GestureRecognizers.Add(tapGestureRecognizer2);

            TapGestureRecognizer tapGestureRecognizer3 = new TapGestureRecognizer();
            tapGestureRecognizer3.Tapped += AbsLayBalance_Tabbed;
            AbsLayBalance.GestureRecognizers.Add(tapGestureRecognizer3);

            TapGestureRecognizer tapGestureRecognizer4 = new TapGestureRecognizer();
            tapGestureRecognizer4.Tapped += AbsLayOverloop_Tabbed;
            AbsLayOverloop.GestureRecognizers.Add(tapGestureRecognizer4);
        }

        private void AbsLayOverloop_Tabbed(object sender, EventArgs e)
        {
            gameId = 3;
            
            Navigation.PushAsync(new SpelOverloop());
        }

        private void AbsLayBalance_Tabbed(object sender, EventArgs e)
        {
            gameId = 2;
            Navigation.PushAsync(new BalanceGame());
        }

        private void AbsLay123piano_Tabbed(object sender, EventArgs e)
        {
            gameId = 1;
            Navigation.PushAsync(new Spel123Piano());
        }

        private void AbsLayBack_Tabbed(object sender, EventArgs e)
        {
            AbsLayBack.Scale = 1.5;
            Navigation.PopAsync();
        }
    }
}
