using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

using Android;
using Android.Bluetooth;
using Android.Content;
using Android.Support.V4.App;
using Smart_bike_G3.Models;
using AndroidX.Core.Content;
using AndroidX.Core.App;


using Xamarin.Forms;
using BluetoothLE.Core;
using BluetoothLE.Droid;

namespace Smart_bike_G3.Droid
{
    [Activity(Label = "Smart_bike_G3", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly string[] Permissions =
{
            Manifest.Permission.Bluetooth,
            Manifest.Permission.BluetoothAdmin,
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
};

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //const int locationPermissionsRequestCode = 1000;

            //var locationPermissions = new[]
            //{
            //    Manifest.Permission.AccessCoarseLocation,
            //    Manifest.Permission.AccessFineLocation
            //};

            //// check if the app has permission to access coarse location
            //var coarseLocationPermissionGranted =
            //    ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation);

            //// check if the app has permission to access fine location
            //var fineLocationPermissionGranted =
            //    ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation);

            //// if either is denied permission, request permission from the user
            //if (coarseLocationPermissionGranted == Permission.Denied ||
            //    fineLocationPermissionGranted == Permission.Denied)
            //{
            //    ActivityCompat.RequestPermissions(this, locationPermissions, locationPermissionsRequestCode);
            //}

            //// Register for broadcasts when a device is discovered





            ////BluetoothDeviceReceiver.Adapter.StartDiscovery();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CheckPermissions();
            DependencyService.Register<IAdapter, Adapter>();

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void CheckPermissions()
        {
            bool minimumPermissionsGranted = true;

            foreach (string permission in Permissions)
            {
                if (CheckSelfPermission(permission) != Permission.Granted)
                {
                    minimumPermissionsGranted = false;
                }
            }

            // If any of the minimum permissions aren't granted, we request them from the user
            if (!minimumPermissionsGranted)
            {
                RequestPermissions(Permissions, 0);
            }
        }

        //[Android.Runtime.Register("onUserInteraction", "()V", "GetOnUserInteractionHandler")]
        //public override void OnUserInteraction()
        //{
        //    Console.WriteLine("USER INTERACTED");
        //}
    }
}