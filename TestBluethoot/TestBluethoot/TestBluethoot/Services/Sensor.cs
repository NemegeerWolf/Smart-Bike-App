using System;
using System.Collections.Generic;
using System.Text;

namespace TestBluethoot.Services
{
    static class Sensor
    {
        static int cadence = 0;
        static  ulong runtime = 0;
        static  ulong last_millis = 0;

        static int prevCumulativeCrankRev = 0;
        static int prevCrankTime = 0;
        static double rpm = 0;
        static double prevRPM = 0;
        static int prevCrankStaleness = 0;
        static int stalenessLimit = 4;
        static int scanCount = 0;

        public static event EventHandler<int> NewDataCadence;
        public static event EventHandler<int> NewDataSpeed;
        public static event EventHandler<bool> NewDataBool;

        private static bool is_bit_set(byte value, int bitindex)
        {
            return (value & (1 << bitindex)) != 0;
        }

        public static double GetSpeed(double diameter)
        {
            return cadence * 2.0 * Math.PI * diameter;
        }

        public static double Getcadence()
        {
            return cadence;
        }

        public static void GotNewdata(object sender, byte[] data)
        {

            bool hasWheel = is_bit_set(data[0], 0);
            bool hasCrank = is_bit_set(data[0], 1);

            int crankRevIndex = 1;
            int crankTimeIndex = 3;
            if (hasWheel)
            {
                crankRevIndex = 7;
                crankTimeIndex = 9;
            }

            int cumulativeCrankRev = (int)((data[crankRevIndex + 1] << 8) + data[crankRevIndex]);
            int lastCrankTime = (int)((data[crankTimeIndex + 1] << 8) + data[crankTimeIndex]);

            //if (debug)
            //{
            //    Serial.println("Notify callback for characteristic");
            //    Serial.print("cumulativeCrankRev: ");
            //    Serial.println(cumulativeCrankRev);
            //    Serial.print("lastCrankTime: ");
            //    Serial.println(lastCrankTime);
            //}

            int deltaRotations = cumulativeCrankRev - prevCumulativeCrankRev;
            if (deltaRotations < 0)
            {
                deltaRotations += 65535;
            }

            int timeDelta = lastCrankTime - prevCrankTime;
            if (timeDelta < 0)
            {
                timeDelta += 65535;
            }

            // In Case Cad Drops, we use PrevRPM 
            // to substitute (up to 4 seconds before reporting 0)
            if (timeDelta != 0)
            {
                prevCrankStaleness = 0;
                double timeMins = ((double)timeDelta) / 1024.0 / 60.0;
                rpm = ((double)deltaRotations) / timeMins;
                prevRPM = rpm;
            }
            else if (timeDelta == 0 && prevCrankStaleness < stalenessLimit)
            {
                rpm = prevRPM;
                prevCrankStaleness += 1;
            }
            else if (prevCrankStaleness >= stalenessLimit)
            {
                rpm = 0.0;
            }

            prevCumulativeCrankRev = cumulativeCrankRev;
            prevCrankTime = lastCrankTime;

            cadence = (int)rpm;
            //Test
            //cadence = cadence + 2;
            NewDataCadence?.Invoke("SelectCharacteristic", cadence);
            NewDataBool?.Invoke("SelectCharacteristic", true);
            NewDataSpeed?.Invoke("SelectCharacteristic", (int) Sensor.GetSpeed(0.05));
        }


    }
}
