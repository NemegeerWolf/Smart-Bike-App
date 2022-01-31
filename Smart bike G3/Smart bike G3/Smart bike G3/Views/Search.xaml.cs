using Smart_bike_G3.Services;
using TestBluethoot.Models;
using TestBluethoot.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_bike_G3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
       
        public Search()
        {
            InitializeComponent();

            
            Bluetooth.StopNotify();
            listView.ItemsSource = Bluetooth.Scan();


          
        }
    
        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var n = (BleList)e.Item;
            
                    var check = await DisplayAlert("", "Connecting to  [" + n.Name+ "]", "ok", "cancel");

                    if (check)
                    {
                        Sensor.SensorName = n.Name;
                        Bluetooth.Connect(n);
                        Navigation.PopToRootAsync();
                    }
           
        }
       
        
        
    }
}
