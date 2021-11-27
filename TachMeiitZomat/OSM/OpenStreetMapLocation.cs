using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachMeiitZomat
{
    public class OpenStreetMapLocation
    {
        public long place_id { get; set; }
        public String licence { get; set; }
        public String osm_type { get; set; }
        public long osm_id { get; set; }
        public String lat { get; set; }
        public String lon { get; set; }
        public String display_name { get; set; }
        public Address address { get; set; }
        public IList<String> boundingbox { get; set; }
    }
}
