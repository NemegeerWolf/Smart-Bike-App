using Quick.Xamarin.BLE.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestBluethoot.Models
{
    public class CharacteristicsList
    {
        
        public string Uuid { get; set; }
        public bool Isread { get; set; }
        public bool Iswrite { get; set; }
        public bool Isnotify { get; set; }
        public string Name { get; set; }
        public CharacteristicsList(string uuid,bool isread, bool iswrite, bool isnotify, string name)
        {
            Isread = isread;
            Iswrite = iswrite;
            Isnotify = isnotify;
            Uuid = uuid;
            Name = name;
        }

    }
}
