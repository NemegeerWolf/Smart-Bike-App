using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBluethoot.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BalanceGame : ContentPage
    {
        
        double speed;
        bool playing = false;
        bool started = false;
        bool stopped = false;
        public BalanceGame()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                InitializeComponent();
                NavigationPage.SetHasNavigationBar(this, false);
                oneWheel.AnchorY = 0.85;
                resumeBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Resume.png");
                quitBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Quit.png");
                StartBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.StartB.png");
                pauseBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.pauze.png");
                pauseBtn.IsVisible = false;
                Animate();
                Device.StartTimer(TimeSpan.FromMilliseconds(100), Animate);

                Sensor.NewDataSpeed += ((s, e) =>
                {
                    speed = e;
                });
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
        }

        
        private bool Animate()
        {
            //Random rand = new Random();
            //speed = rand.Next(0, 30);
            double speedval;
            int angle;

            if (playing)
            {
                speedval = CheckSpeedLimit(speed);
                double targetSpeed = 15;
                uint fallSpeed = CalcFallSpeed(speedval, targetSpeed);

                if (speedval == targetSpeed)
                {
                    Rotate(0, 3000);
                }
                else
                {
                    angle = SetAngle(speedval, targetSpeed);
                    Rotate(angle, fallSpeed);
                }
            }
            else
            {
                if (started)
                {
                    Rotate((int)oneWheel.Rotation, 0);

                }
                else
                {
                    Rotate(0, 0);
                }

            }
            if (stopped)
            {
                return false;
            }
            return true;
        }

        private uint CalcFallSpeed(double speedval, double targetSpeed)
        {
            double difference = Math.Abs(targetSpeed - speedval);
            double percentage = Math.Round(((targetSpeed - difference) / targetSpeed) * 100);
            return Convert.ToUInt32(Math.Abs((percentage) * 100) + 1000);
        }

        private double CheckSpeedLimit(double speedval)
        {
            if (speedval > 30)
            {
                return 30;
            }
            else
            {
                return speedval;
            }
        }

        private int SetAngle(double speedval ,double targetSpeed)
        {
            if (speedval < targetSpeed)
            {
                return 90;
            }
            else
            {
                return -90;
            }
        }

        private async Task Rotate(int degrees, uint speed)
        {
            await oneWheel.RotateTo(degrees, speed);
            if (Math.Round(oneWheel.Rotation) == 80 || Math.Round(oneWheel.Rotation) == -80)
            {
                Debug.WriteLine("dead");
            }
        }

        private void pauseBtn_Clicked(object sender, EventArgs e)
        {
            playing = false;
            pauseBtn.IsVisible = false;
            pauzedFrame.IsVisible = true;
        }

        private void StartBtn_Clicked(object sender, EventArgs e)
        {
            started = true;
            playing = true;
            StartBtn.IsVisible = false;
            pauseBtn.IsVisible = true;
        }

        private void ResumeBtn_Clicked(object sender, EventArgs e)
        {
            playing = true;
            pauseBtn.IsVisible = true;
            pauzedFrame.IsVisible = false;
        }

        private void QuitBtn_Clicked(object sender, EventArgs e)
        {
            stopped = true;
            Navigation.PopAsync();
        }
    }
}
