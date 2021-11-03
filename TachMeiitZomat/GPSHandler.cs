using System;
using System.Device.Location;
using System.IO.Ports;
using System.Net.Http;
using Newtonsoft.Json;


namespace TachMeiitZomat
{
    class GPSHandler : IDisposable
    {
        SerialPort port = new SerialPort();
        GeoCoordinate coordinate = new GeoCoordinate();
        HttpClient client = new HttpClient();
        bool ready = false;

        public GPSHandler(string comPort)
        {
            initComPort(comPort);
            client.DefaultRequestHeaders.Add("Referer", "TachMeiitZomat");
        }

        public void Dispose()
        {
            client.Dispose();
            port.Close();
            port.Dispose();
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
            // only parse gprmc messages
            if (message.Contains("GPRMC"))
            {
                var gprmcMessage = new GPRMC(message);

                // only convert lat and lng if both are not null
                if (gprmcMessage.Latitude != "" && gprmcMessage.Longitude != "")
                {
                    double degreeLat = Convert.ToDouble(gprmcMessage.Latitude.Substring(0, 2));
                    degreeLat += Convert.ToDouble(gprmcMessage.Latitude.Substring(2).Replace(".", ",")) / 60;

                    double degreeLng = Convert.ToDouble(gprmcMessage.Longitude.Substring(0, 3));
                    degreeLng += Convert.ToDouble(gprmcMessage.Longitude.Substring(3).Replace(".", ",")) / 60;

                    coordinate.Latitude = degreeLat;
                    coordinate.Longitude = degreeLng;
                    coordinate.Speed = Convert.ToDouble(gprmcMessage.Speed.Replace(".", ","));
                }
            }
        }


        public double getSpeed()
        {
            // 3.6 is the factor for m/s to km/h
            return Math.Round((coordinate.Speed != double.NaN ? coordinate.Speed * 1.852 : 0));
        }

        /// <summary>
        /// Returns the county of the current position saved in the gps handler
        /// if no county is set, the current city is returned. 
        /// if no city is set too, "unbekannt" will be returned
        /// </summary>
        /// <returns></returns>
        public string getCountyOrCity()
        {
            var response = client.GetAsync("http://nominatim.openstreetmap.org/reverse?format=json"
                + "&lat=" + coordinate.Latitude.ToString().Replace(",", ".")
                + "&lon=" + coordinate.Longitude.ToString().Replace(",", ".")).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;
                var location = JsonConvert.DeserializeObject<OpenStreetMapLocation>(responseString);
                if (location.address.county != null)
                {
                    return location.address.county;
                }
                else if (location.address.city != null)
                {
                    return location.address.city;
                }
            }
            return "unbekannt";
        }

        public bool getReady()
        {
            return ready;
        }
    }
}
