using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachMeiitZomat
{
    public class Address
    {
        /*
         * fields regarding to
         * https://nominatim.org/release-docs/develop/api/Output/#addressdetails
         */
        public string continent { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string region { get; set; }
        public string state { get; set; }
        public string state_district { get; set; }
        public string county { get; set; }
        public string municipality { get; set; }
        public string city { get; set; }
        public string town { get; set; }
        public string village { get; set; }
        public string city_district { get; set; }
        public string district { get; set; }
        public string borough { get; set; }
        public string suburb { get; set; }
        public string subdivision { get; set; }
        public string hamlet { get; set; }
        public string croft { get; set; }
        public string isolated_dwelling { get; set; }
        public string neighbourhood { get; set; }
        public string allotments { get; set; }
        public string quarter { get; set; }
        public string city_block { get; set; }
        public string residental { get; set; }
        public string farm { get; set; }
        public string farmyard { get; set; }
        public string industrial { get; set; }
        public string commercial { get; set; }
        public string retail { get; set; }
        public string road { get; set; }
        public string house_number { get; set; }
        public string house_name { get; set; }
        public string emergency { get; set; }
        public string historic { get; set; }
        public string military { get; set; }
        public string natural { get; set; }
        public string landuse { get; set; }
        public string place { get; set; }
        public string railway { get; set; }
        public string man_made { get; set; }
        public string aerialway { get; set; }
        public string boundary { get; set; }
        public string amenity { get; set; }
        public string aeroway { get; set; }
        public string club { get; set; }
        public string craft { get; set; }
        public string leisure { get; set; }
        public string office { get; set; }
        public string mountain_pass { get; set; }
        public string shop { get; set; }
        public string tourism { get; set; }
        public string bridge { get; set; }
        public string tunnel { get; set; }
        public string waterway { get; set; }
        public string postcode { get; set; }
    }
}
