using System;
using System.Device.Location;
using System.IO.Ports;

namespace TachMeiitZomat
{
    class GpsSensor : IDisposable
    {
        SerialPort port = new SerialPort();
        string Message = "";
        GeoCoordinate GeoCoordinateCache = new GeoCoordinate();

        public GpsSensor(String COMport)
        {
            port.BaudRate = 4800;
            port.DataBits = 8;
            port.Parity = Parity.None;
            port.StopBits = StopBits.One;
            port.PortName = COMport;
        }

        public void ReadLoop()
        {
            port.Open();
            while (true)
            {
                try
                {
                    String _message = port.ReadLine();
                    if (_message.Contains("GPRMC"))
                    {
                        Message = _message;
                    }
                } catch(Exception ex)
                {
                    // Do nothing
                }
            }
        }

        public void Dispose()
        {
            port.Close();
        }

        public GeoCoordinate GetCoordinate()
        {
            var cord = ParseGPRMCMessage(Message);
            if (!cord.IsUnknown)
            {
                GeoCoordinateCache.Latitude = cord.Latitude;
                GeoCoordinateCache.Longitude = cord.Longitude;
            }
            if (cord.Speed != double.NaN)
            {
                GeoCoordinateCache.Speed = cord.Speed;
            }
            return GeoCoordinateCache;
        }

        private GeoCoordinate ParseGPRMCMessage(string message)
        {
            var Coordinate = new GeoCoordinate();
            Coordinate.Speed = double.NaN;
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


                    Coordinate.Latitude = degreeLat;
                    Coordinate.Longitude = degreeLng;
                }
                if(gprmcMessage.Speed != "")
                {
                    var knots = Convert.ToDouble(gprmcMessage.Speed.Replace(".", ","));
                    Coordinate.Speed = Math.Round(knots != double.NaN ? knots * 1.852 : 0);

                }
            }
            return Coordinate;
        }

    }
}
