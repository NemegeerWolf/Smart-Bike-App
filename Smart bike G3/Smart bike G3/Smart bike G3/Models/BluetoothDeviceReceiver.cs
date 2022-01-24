using System;
using System.Collections.Generic;
using Android.Bluetooth;
using Android.Content;
using Smart_bike_G3.Services;

namespace Smart_bike_G3.Models
{
    public class BluetoothDeviceReceiver : BroadcastReceiver
    {
        public static BluetoothAdapter Adapter = Bluetooth.adapter;
        public static List<BluetoothDevice> UnPairedBluetoothDevices;
        public override void OnReceive(Context context, Intent intent)
        {
            var action = intent.Action;

            if (action != BluetoothDevice.ActionFound)
            {
                return;
            }

            // Get the device
            var device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);

            if (device.BondState != Bond.Bonded)
            {
                UnPairedBluetoothDevices.Add(device);
                Console.WriteLine($"Found device with name: {device.Name} and MAC address: {device.Address}");
            }
        }
    }
}
