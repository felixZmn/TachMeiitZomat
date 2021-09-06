using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachMeiitZomat
{
    class GPRMC
    {
        public string RawMessage { get; private set; }
        public string TimeOfFix { get; private set; }
        public string Validity { get; private set; }
        public string Latitude { get; private set; }
        public string NorthSouth { get; private set; }
        public string Longitude { get; private set; }
        public string EastWest { get; private set; }
        public string Speed { get; private set; }
        public string TrueCourse { get; private set; }
        public string DateStamp { get; private set; }
        public string Variation { get; private set; }
        public string VariationDirection { get; private set; }
        public string Mode { get; private set; }
        public string Checksum { get; private set; }

        public GPRMC(string RawMessage)
        {
            this.RawMessage = RawMessage;

            string pattern = @"\$GPRMC,(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?),(.*?)\*(.*)";
            var matches = System.Text.RegularExpressions.Regex.Matches(RawMessage, pattern);

            if (matches.Count == 1)
            {
                var groups = matches[0].Groups;
                
                TimeOfFix = groups[1].ToString();
                Validity = groups[2].ToString();
                Latitude = groups[3].ToString();
                NorthSouth = groups[4].ToString();
                Longitude = groups[5].ToString();
                EastWest = groups[6].ToString();
                Speed = groups[7].ToString();
                TrueCourse = groups[8].ToString();
                DateStamp = groups[9].ToString();
                Variation = groups[10].ToString();
                VariationDirection = groups[11].ToString();
                Checksum = groups[12].ToString();
                Checksum = groups[13].ToString();
            }
        }
    }
}
