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

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Spel123Piano : ContentPage
    {//public variables

        public double Distance { get; set; } = 0;
        public bool GameOver { get; set; } = false;

        // local variables
        List<Xamarin.Forms.Shapes.Rectangle> wayMarks = new List<Xamarin.Forms.Shapes.Rectangle>();
        private bool started;
        private double width;
        private double height;
        private double gap;
        private DisplayInfo mainDisplayInfo;
        private double globalSpeed = 100;
        Random random = new Random(Convert.ToInt32(DateTime.Now.Millisecond));

        private DateTime startOrange = DateTime.MinValue;
        private DateTime startRed = DateTime.MinValue;

        public bool IsRed { get; private set; }

        public Spel123Piano()
        {
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromMilliseconds(10.0), Streetmove);

            Device.StartTimer(TimeSpan.FromMilliseconds(10.0), GamePlay);
        }

        private bool GamePlay()
        {
            globalSpeed = 30;// read sensor here


            // lights
            if (startRed == DateTime.MinValue && startOrange == DateTime.MinValue)
            {
                int luck = random.Next(0, 80);
                if (luck == 1)
                {
                    circGreen.Opacity = 0.5;
                    circOrange.Opacity = 1;
                    startOrange = DateTime.Now;
                    
                    Debug.WriteLine("orange");

                }
            }

            if (startOrange.AddSeconds(2) < DateTime.Now && startOrange != DateTime.MinValue)
            {
                startOrange = DateTime.MinValue;
                startRed = DateTime.Now;
                circOrange.Opacity = 0.5;
                circRed.Opacity = 1;
                IsRed = true;
                Debug.WriteLine("rood");
            }

            if (startRed.AddSeconds(8) < DateTime.Now && startRed != DateTime.MinValue)
            {
                startRed = DateTime.MinValue;
                circRed.Opacity = 0.5;
                circGreen.Opacity = 1;
                IsRed = false;
                Debug.WriteLine("groen");
            }

            //game over went speed more than 1km/u

            if (IsRed == true && globalSpeed > 1)
            {

                lblGameOver.IsVisible = true;
                btnRestart.IsVisible = true;

                //sent to API
                //Repository.AddResultsGame(1, Name.User, Convert.ToInt32( Distance), 0); // desable for not filling the database 
                Navigation.PushAsync(new Scorebord(Convert.ToInt32(Distance))); // push to scoreboard
                return false;

            }
            //Distance += speed * (2.77777778 * Math.Pow(10, -5)); // km
            Distance += globalSpeed * 0.0277777778; // meter
            lblscore.Text = $"{Math.Round(Distance, 4).ToString()} m";


            return true;
        }

        private bool Streetmove()
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
                            rectangle.Layout(new Rectangle(-width * 0.3 / 2, -height + gap / (200 / speed), width * 0.2, height));
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
            Device.StartTimer(TimeSpan.FromMilliseconds(10.0), Streetmove);

            Device.StartTimer(TimeSpan.FromMilliseconds(10.0), GamePlay);
        }
    }
}
 
