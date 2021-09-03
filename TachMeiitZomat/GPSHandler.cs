using System;
using System.Device.Location;
using System.IO.Ports;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;


namespace TachMeiitZomat
{
    class GPSHandler
    {
        SerialPort port = new SerialPort();
        GeoCoordinate coordinate = new GeoCoordinate();
        HttpClient client = new HttpClient();
        bool ready = false;

        public GPSHandler(string comPort)
        {
            // ToDo: set com port name according to settings file
            initComPort(comPort);
            client.DefaultRequestHeaders.Add("Referer", "TachMeiitZomat");
        }

        private void initComPort(string portName)
        {
            port.BaudRate = 4800;
            port.DataBits = 8;
            port.Parity = Parity.None;
            port.StopBits = StopBits.One;
            port.PortName = portName;
            try
            {
                port.Open();
                ready = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void ReadGpsSensor()
        {
            while (true)
            {
                Form1.mre.WaitOne();
                string message = port.ReadLine();
                parseNMEAMessage(message);
            }
        }

        private void parseNMEAMessage(string message)
        {
            string pattern = @"\$GPRMC,(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?)\*(.*)";
            var matches = Regex.Matches(message, pattern);
            
            if (matches.Count == 1)
            {
                string lat = matches[0].Groups[3].ToString();
                string lng = matches[0].Groups[5].ToString();
                string spd = matches[0].Groups[7].ToString();

                double degreeLat = Convert.ToDouble(lat.Substring(0, 2));
                degreeLat += Convert.ToDouble(lat.Substring(2).Replace(".", ",")) / 60;

                double degreeLng = Convert.ToDouble(lng.Substring(0, 3));
                degreeLng += Convert.ToDouble(lng.Substring(2).Replace(".", ",")) / 60;

                Console.WriteLine(message);
                Console.WriteLine("parsed data:");
                Console.WriteLine(degreeLat);
                Console.WriteLine(degreeLng);
                Console.WriteLine(spd);


                coordinate.Latitude = degreeLat;
                coordinate.Longitude = degreeLng;
                coordinate.Speed = Convert.ToDouble(spd.Replace(".", ","));
            }
        }

        public double getSpeed()
        {
            // 3.6 is the factor for m/s to km/h
            return coordinate.Speed != double.NaN ? coordinate.Speed * 3.6 : 0;
        }

        public String getCounty()
        { 
            
            var response = client.GetAsync("http://nominatim.openstreetmap.org/reverse?format=json" 
                + "&lat=" + coordinate.Latitude.ToString().Replace(",", ".") 
                + "&lon=" + coordinate.Longitude.ToString().Replace(",", ".")).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<OpenStreetMapLocation>(responseString).address.county;
            }
            return "";
        }
        
        public bool getReady()
        {
            return ready;
        }
    }
}
