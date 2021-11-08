using System.Device.Location;
using System.Net.Http;
using System.Threading.Tasks;

namespace TachMeiitZomat
{
    class CoordinateResolver
    {
        async public static Task<string> Resolve(GeoCoordinate Coordinate)
        {
            using (var Client = new HttpClient())
            {
                Client.DefaultRequestHeaders.Add("Referer", "TachMeiitZomat");
                var url = "http://nominatim.openstreetmap.org/reverse?format=json"
            + "&lat=" + Coordinate.Latitude.ToString().Replace(",", ".")
            + "&lon=" + Coordinate.Longitude.ToString().Replace(",", ".");

                return await Client.GetStringAsync(url);
            }
        }
    }
}
