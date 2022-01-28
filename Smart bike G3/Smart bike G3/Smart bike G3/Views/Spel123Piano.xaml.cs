using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Xamarin.Essentials;
using Smart_bike_G3.Repositories;
using TestBluethoot.Services;

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Spel123Piano : ContentPage
    {//public variables
        public double Time = 0;
        public double Distance { get; set; } = 1000; // 1000m / 1km
        public bool GameOver { get; set; } = false;

        // local variables


        string[] gameovers = new string[] {"Jammer, geef niet op", "Niet opgeven, volgende keer beter","Probeer opnieuw", "Winnen, da’s iets voor losers.", "Je kan veel winnen als je van je verlies leert.", "Een glimlach om een nederlaag is een eindoverwinning.", "Bijna elke overwinning begint met een nederlaag", "Gefeliciteerd, Je reed door het rood." };

        List<Xamarin.Forms.Shapes.Rectangle> wayMarks = new List<Xamarin.Forms.Shapes.Rectangle>();
        private bool started;
        private double width;
        private double height;
        private double gap;
        private DisplayInfo mainDisplayInfo;
        private double globalSpeed = 10;

        //Luck of Time
        //Random random = new Random(Convert.ToInt32(DateTime.Now.Millisecond));

        //Luck of the Devil
        Random random = new Random(13);

        private DateTime startOrange = DateTime.MinValue;
        private DateTime startRed = DateTime.MinValue;
        private bool IsPauzed = false;

        public bool IsRed { get; private set; }

        public Spel123Piano()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            { 
                InitializeComponent();

                pictures();

                TapGestureRecognizer recognizer = new TapGestureRecognizer();
                recognizer.Tapped += btnPauze_Clicked;
                btnPauze.GestureRecognizers.Add(recognizer);
                Pauze.GestureRecognizers.Add(recognizer);
                Play.GestureRecognizers.Add(recognizer);

                imagetest.Source= ImageSource.FromResource(@"Smart_bike_G3.Assets.Home.png");
                image.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Again.png");

                Device.StartTimer(TimeSpan.FromMilliseconds(10.0), Streetmove);

                Device.StartTimer(TimeSpan.FromMilliseconds(10.0), GamePlay);
                Device.StartTimer(TimeSpan.FromSeconds(1), ChangeTime);

                // If there is new data -> Read sensor
                Sensor.NewDataSpeed += ((s, e) =>
                {
                    globalSpeed = e;
                });
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
            
        }

        private void btnPauze_Clicked(object sender, EventArgs e)
        {
            if (IsPauzed)
            {
                IsPauzed = false;
                
            }
            else
            {
                IsPauzed = true;
            }
            Pauze.IsVisible = !IsPauzed;
            Play.IsVisible = IsPauzed;
        }

        private void pictures()
        {
            ImgGreen1.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Green1.png");
            ImgGreen2.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Green2.png");
            ImgGreen3.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Green3.png");
            ImgGreen4.Source = ImageSource.FromResource(@"Smart_bike_G3.Assets.Green4.png");
        }

        private bool ChangeTime()
        {
            if (!IsPauzed)
            {
                if (lblGameOver.IsVisible == false)
                {
                    Time += 1;
                    var dateTime = DateTime.MinValue.AddSeconds(Time);
                    if (dateTime.Minute >= 1)
                    {
                        lblTime.Text = $"{dateTime.Minute}min{dateTime.Second}";
                        return true;

                    }
                    lblTime.Text = $"{dateTime.Second} sec";
                    return true;
                }
                return false;
            }
            return true;

        }

        private bool GamePlay()
        {
            if (!IsPauzed)
            {



                // lights
                if (startRed == DateTime.MinValue && startOrange == DateTime.MinValue)
            {
                int luck = random.Next(0, 110);
                if (luck == 1)
                {
                    circGreen.Opacity = 0.5;
                    circOrange.Opacity = 1;
                    startOrange = DateTime.Now;
                    
                    Debug.WriteLine("orange");

                }
            }

            if (startOrange.AddSeconds(3) < DateTime.Now && startOrange != DateTime.MinValue)
            {
                startOrange = DateTime.MinValue;
                startRed = DateTime.Now;
                circOrange.Opacity = 0.5;
                circRed.Opacity = 1;
                IsRed = true;
                Debug.WriteLine("rood");
            }

            if (startRed.AddSeconds(3) < DateTime.Now && startRed != DateTime.MinValue)
            {
                startRed = DateTime.MinValue;
                circRed.Opacity = 0.5;
                circGreen.Opacity = 1;
                IsRed = false;
                Debug.WriteLine("groen");
            }

            //game over went speed more than 1km/u

            if (IsRed == true && globalSpeed > 5)
            {
                lblGameOver.Text = gameovers[random.Next(0, gameovers.Length)];
                lblGameOver.IsVisible = true;
                btnRestart.IsVisible = true;
                btnRestart.IsEnabled = true;
                btnHome.IsVisible = true;
                //btnRestartText.Text = $"Opnieuw";
                //sent to API

                //Repository.AddResultsGame(1, Name.User, Convert.ToInt32(Distance), 0); // desable for not filling the database 

                //Navigation.PushAsync(new Scorebord(Convert.ToInt32(0))); // push to scoreboard
                return false;

            }

            //Distance += speed * (2.77777778 * Math.Pow(10, -5)); // km
            Distance -= globalSpeed * 0.0277777778; // meter
            lblscore.Text = $"{Math.Round(Distance, 0).ToString()} m";
            lblSpeed.Text = globalSpeed.ToString();
            // if finished ...
            if (Distance < 0)
            {
                lblGameOver.Text = "YOU WIN";
                lblGameOver.TextColor = Brush.Green.Color;

                lblscore.Text = "0 m";
                lblGameOver.IsVisible = true;
                btnRestart.IsVisible = true;

                var dateTime = DateTime.MinValue.AddSeconds(Time);
                btnRestartText.Text = $"{dateTime.Minute}min{dateTime.Second}";
                btnRestart.IsEnabled = false;
                
                Repository.AddResultsGame(1, Convert.ToInt32(Time), 0);
                Thread.Sleep(3000);
                Navigation.PushAsync(new Scorebord(Convert.ToInt32(Time))); // push to scoreboard
                return false;
            }

            return true;
            }
            return true;
        }

        private bool Streetmove()
        {
            if (!IsPauzed)
            {
                if (!lblGameOver.IsVisible)
                {
                    mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
                    DeviceDisplay.MainDisplayInfoChanged += ((l, m) =>
                    {
                        Debug.WriteLine("Hellloooow wolfffiieeieieieiei");
                        if (mainDisplayInfo.Height != DeviceDisplay.MainDisplayInfo.Height)
                        {
                            loadingIndicator.IsRunning = true;
                            setup();
                            Thread.Sleep(3000);
                            setup();
                            loadingIndicator.IsRunning = false;
                        }



                    });

                    if (!started)
                    {
                        started = true;
                        loadingIndicator.IsRunning = true;
                        setup();
                        loadingIndicator.IsRunning = false;

                    }
                    double speed = globalSpeed;
                    if (!(speed <= 0))
                    {

                        foreach (Xamarin.Forms.Shapes.Rectangle rectangle in wayMarks)
                        {




                            if (rectangle.Y > gap * 6)
                            {
                                if (Distance < 30 && Distance > 27)
                                {
                                    rectangle.Layout(new Rectangle(-width * 0.3 / 2, -height + gap / (200 / speed), width * 6, 20));
                                }
                                else if (Distance > 35)
                                {
                                    rectangle.Layout(new Rectangle(-width * 0.3 / 2, -height + gap / (200 / speed), width * 0.2, height));
                                }

                            }
                            else
                            {

                                rectangle.Layout(new Rectangle(-rectangle.Width / 2, rectangle.Y + gap / (200 / speed), rectangle.Width + rectangle.Width * (0.001 / (1.0 / speed)), rectangle.Height - rectangle.Height * (0.00025 / (1.0 / speed))));
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void setup()
        {


            mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            wayMarks.Clear();
            cnv.Children.Clear();
            Device.BeginInvokeOnMainThread(() =>
            {
                loadingIndicator.IsRunning = true;
                for (int i = 0; i < 7; i++)
                {


                    Xamarin.Forms.Shapes.Rectangle baseRect = new Xamarin.Forms.Shapes.Rectangle();
                    baseRect.Fill = Brush.White;



                    if (mainDisplayInfo.Height > mainDisplayInfo.Width)
                    {
                        width = Convert.ToInt32((mainDisplayInfo.Width / mainDisplayInfo.Density) * 0.05);
                        height = Convert.ToInt32((mainDisplayInfo.Width / mainDisplayInfo.Density) * 0.20);
                        gap = height * 1.2;
                    }
                    else
                    {
                        width = Convert.ToInt32((mainDisplayInfo.Width / mainDisplayInfo.Density) * 0.05);
                        height = Convert.ToInt32((mainDisplayInfo.Width / mainDisplayInfo.Density) * 0.10);
                        gap = height * 1.2;
                    }
                    double width1 = width * 0.3;
                    double height1 = height;
                    for (int e = 0; e <= i; e++)
                    {
                        width1 = width1 + width1 * 0.03;
                        height1 = height1 - height1 * 0.005;
                    }
                    var re = new Rectangle(0, 110 * i, 20, 50);
                    baseRect.LayoutTo(new Rectangle(-width1 / 2, gap * i, width1, height1));

                    AbsoluteLayout.SetLayoutFlags(baseRect, AbsoluteLayoutFlags.None);
                    wayMarks.Add(baseRect);
                    cnv.Children.Add(baseRect);

                }
                loadingIndicator.IsRunning = false;
            });

        }

        private void btnRestart_Clicked(object sender, EventArgs e)
        {
            setup();

            lblGameOver.IsVisible = false;
            btnRestart.IsVisible = false;
            btnHome.IsVisible = false;
            Distance = 1000;
            Time = 0;

            Device.StartTimer(TimeSpan.FromMilliseconds(10.0), Streetmove);

            Device.StartTimer(TimeSpan.FromMilliseconds(10.0), GamePlay);

            Device.StartTimer(TimeSpan.FromSeconds(1), ChangeTime);
        }

        private void btnHome_Clicked(object sender, EventArgs e)
        {
            //Navigation.PopToRootAsync(); // root page moet veranderd worden.
            Navigation.PushAsync(new VideoOrGame());
        }
    }
}
 
