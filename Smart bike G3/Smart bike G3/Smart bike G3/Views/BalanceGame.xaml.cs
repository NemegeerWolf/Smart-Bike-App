using Smart_bike_G3.Models;
using Smart_bike_G3.Repositories;
using System;
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
        int seconds;
        DateTime time;
        string timeStr;
        bool playing = false;
        bool started = false;
        bool stopped = false;
        int countdown = 1;
        public BalanceGame()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                InitializeComponent();

                //prevent sleepmode
                
                DeviceDisplay.KeepScreenOn = true;

                NavigationPage.SetHasNavigationBar(this, false);
                SetXaml();
                Device.StartTimer(TimeSpan.FromMilliseconds(100), Animate);
                Device.StartTimer(TimeSpan.FromMilliseconds(100), CheckDead);

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


        private bool Timer()
        {
            if (!stopped)
            {
                if (playing)
                {
                    seconds += 1;
                    time = DateTime.MinValue.AddSeconds(seconds);
                    timePassdlbl.Text = TimeForDisplay(time);
                    if (time.Minute >= 1)
                    {
                        timeStr = $"{time.Minute}min{time.Second}";
                        return true;

                    }
                    timeStr = $"{time.Second} sec";
                    return true;
                }
                return true;
            }
            return false;
        }

        private void SetXaml()
        {
            oneWheel.AnchorY = 0.85;
            pauseBtn.IsVisible = false;
            timePassdlbl.Text = "00:00";
            homeBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Home.png");
            resumeBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Resume.png");
            quitBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Quit.png");
            StartBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.StartBl.png");
            pauseBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.pauze.png");
        }

        private bool Animate()
        {
            //Random rand = new Random();
            //speed = rand.Next(0, 30);
            double speedval;
            int angle;
            if (!stopped)
            {
                if (playing)
                {
                    speedval = CheckSpeedLimit(speed);
                    double targetSpeed = 15;
                    uint fallSpeed = CalcFallSpeed(speedval, targetSpeed);
                    SetFeedback();
                    if (speedval == targetSpeed)
                    {
                        Rotate(0, 3000);
                        return true;
                    }
                    else
                    {
                        angle = SetAngle(speedval, targetSpeed);
                        Rotate(angle, fallSpeed);
                        return true;
                    }
                }
                else
                {
                    if (started)
                    {
                        Rotate((int)oneWheel.Rotation, 0);
                        return true;

                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void SetFeedback()
        {
            double Angle = Math.Round(oneWheel.Rotation);
            if (Angle > 10)
            {
                feedbacklbl.Text = "Sneller!!";
            }
            else if (Angle < -10)
            {
                feedbacklbl.Text = "Trager!!";
            }
            else
            {
                feedbacklbl.Text = "Goed";
            }
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
        }

        private bool CheckDead()
        {
            if (Math.Round(oneWheel.Rotation) == 80 || Math.Round(oneWheel.Rotation) == -80)
            {
                playing = false;
                stopped = true;
                feedbacklbl.IsVisible = false;
                timePassdlbl.IsVisible = false;
                timerlbl.Text = $"{timeStr} overleefd";
                timerlbl.IsVisible = true;
                started = false;
                Repository.AddResultsGame(2, Convert.ToInt32(seconds), 0);
                Device.StartTimer(TimeSpan.FromMilliseconds(2500), Reset);
                Device.StartTimer(TimeSpan.FromMilliseconds(2000), Navigate);
                return false;
            }
            return true;
        }

        private bool Reset()
        {
            Rotate(0, 0);
            homeBtn.IsVisible = true;

            timePassdlbl.Text = "00:00";
            stopped = false;
            StartBtn.IsVisible = true;
            pauseBtn.IsVisible = false;
            feedbacklbl.IsVisible = false;
            timerlbl.IsVisible = false;
            seconds = 0;
            Device.StartTimer(TimeSpan.FromMilliseconds(100), Animate);
            Device.StartTimer(TimeSpan.FromMilliseconds(100), CheckDead);
            return false;

        }

        private bool Navigate()
        {
            Navigation.PushAsync(new Scorebord(seconds));
            return false;
        }

        private string TimeForDisplay(DateTime time)
        {
            string minute = CheckDigits(time.Minute.ToString());
            string second = CheckDigits(time.Second.ToString());
            return $"{minute}:{second}";
        }

        private string CheckDigits(string str)
        {
            if (str.Length == 1)
            {
                return $"0{str}";
            }
            return str;
        }

        private void PauseBtn_Clicked(object sender, EventArgs e)
        {
            playing = false;
            pauseBtn.IsVisible = false;
            GridPauseBackGround.IsVisible = true;
            GridPause.IsVisible = true;
            feedbacklbl.IsVisible = false;
        }

        private void StartBtn_Clicked(object sender, EventArgs e)
        {
            StartBtn.IsVisible = false;
            timerlbl.IsVisible = true;
            timerlbl.Text = "3";
            Device.StartTimer(TimeSpan.FromMilliseconds(1000), Countdown);
        }

        private bool Countdown()
        {
            if (countdown == 1)
            {
                countdown = 2;
                timerlbl.Text = "2";
                return true;
            }
            else if (countdown == 2)
            {
                countdown = 3;
                timerlbl.Text = "1";
                return true;
            }
            else if (countdown == 3)
            {
                countdown = 4;
                timerlbl.Text = "GO!!";
                return true;
            }
            else
            {
                countdown = 1;
                timerlbl.IsVisible = false;
                Start();
                return false;
            }
        }

        private void Start()
        {
            timePassdlbl.IsVisible = true;
            started = true;
            playing = true;
            pauseBtn.IsVisible = true;
            feedbacklbl.IsVisible = true;
            homeBtn.IsVisible = false;

            Device.StartTimer(TimeSpan.FromSeconds(1), Timer);
        }

        private void ResumeBtn_Clicked(object sender, EventArgs e)
        {
            GridPauseBackGround.IsVisible = false;
            GridPause.IsVisible = false;
            timerlbl.IsVisible = true;
            timerlbl.Text = "3";
            Device.StartTimer(TimeSpan.FromMilliseconds(1000), Countdown);
        }

        private async void QuitBtn_Clicked(object sender, EventArgs e)
        {
            stopped = true;
            started = false;
            Game lastuser = await Repository.GetLastUserAsync();
            if (lastuser.User == null)
            {
                await Repository.DeleteAsync(lastuser.id);
            }
            Navigation.PopAsync();
        }
    }
}
