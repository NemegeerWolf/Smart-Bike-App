using Smart_bike_G3.Models;
using Smart_bike_G3.Repositories;
using System;
using System.Diagnostics;
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
        bool playing = false;
        bool started = false;
        bool stopped = false;
        int countdown = 1;
        public BalanceGame()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                InitializeComponent();
                NavigationPage.SetHasNavigationBar(this, false);
                SetXaml();
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

        private bool Timer()
        {
            if (!stopped)
            {
                if (playing)
                {
                    seconds += 1;
                    time = DateTime.MinValue.AddSeconds(seconds);
                    timelbl.Text = time.ToString();
                    if (time.Second >= 60)
                    {
                        timelbl.Text = $"{time.Minute}min{time.Second}";
                        return true;

                    }
                    timelbl.Text = $"{time.Second} sec";
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
            resumeBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Resume.png");
            quitBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Quit.png");
            StartBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.StartB.png");
            pauseBtn.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.pauze.png");
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
                SetFeedback();
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

        private void SetFeedback()
        {
            double Angle = Math.Round(oneWheel.Rotation);
            if (Angle > 10)
            {
                feedbacklbl.Text = "Rapper!!";
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
            CheckDead();
        }

        private void CheckDead()
        {
            if (Math.Round(oneWheel.Rotation) == 80 || Math.Round(oneWheel.Rotation) == -80)
            {
                playing = false;
                stopped = true;
                timerlbl.Text = TimeForBord(time);
                timerlbl.IsVisible = true;
            }
        }

        private string TimeForBord(DateTime time)
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
            pauzedFrame.IsVisible = true;
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
            started = true;
            playing = true;
            pauseBtn.IsVisible = true;
            feedbacklbl.IsVisible = true; 
            Device.StartTimer(TimeSpan.FromSeconds(1), Timer);
        }

        private void ResumeBtn_Clicked(object sender, EventArgs e)
        {
            pauzedFrame.IsVisible = false;
            timerlbl.IsVisible = true;
            timerlbl.Text = "3";
            Device.StartTimer(TimeSpan.FromMilliseconds(1000), Countdown);
        }

        private async void QuitBtn_Clicked(object sender, EventArgs e)
        {
            stopped = true;
            Game lastuser = await Repository.GetLastUserAsync();
            if (lastuser.User == null)
            {
                await Repository.DeleteAsync(lastuser.id);
            }
            Navigation.PopAsync();
        }
    }
}
