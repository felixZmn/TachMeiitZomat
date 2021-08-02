using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;

namespace TachMeiitZomat
{
    public partial class Form1 : Form
    {
        HttpClient client = new HttpClient();
        public Form1()
        {
            InitializeComponent();
            client.DefaultRequestHeaders.Add("Referer", "TachMeiitZomat");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateSpeed(00.0);
            updateCounty("Blubbkreis");
        }

        private void updateSpeed(double speed)
        {
            labelSpeed.Text = speed.ToString() + " km/h";
        }

        private void updateCounty(String county)
        {
            labelCounty.Text = county;
        }

        private OpenStreetMapLocation getLocation(String lat, String lon)
        {
            if (true)
            {
                var json = @"{
  'place_id': 203300934,
  'licence': 'Data © OpenStreetMap contributors, ODbL 1.0. https://osm.org/copyright',
  'osm_type': 'way',
  'osm_id': 534929132,
  'lat': '48.71664605',
  'lon': '10.434111758561926',
  'display_name': '36, Freibergstraße, Eglingen, Dischingen, Landkreis Heidenheim, Baden-Württemberg, 89561, Deutschland',
  'address': {
    'house_number': '36',
    'road': 'Freibergstraße',
    'village': 'Eglingen',
    'county': 'Landkreis Neumarkt in der Oberpfalz',
    'state': 'Baden-Württemberg',
    'postcode': '89561',
    'country': 'Deutschland',
    'country_code': 'de'
  },
  'boundingbox': [
    '48.71659',
    '48.716702',
    '10.4340396',
    '10.4341839'
  ]
}";
                return JsonConvert.DeserializeObject<OpenStreetMapLocation>(json);

            }
            var response = client.GetAsync("http://nominatim.openstreetmap.org/reverse?format=json&lat=" + lat + "&lon=" + lon).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<OpenStreetMapLocation>(responseString);
            }
            else
            {
                return null;
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var location = getLocation("48.716669", "10.434100");
            updateSpeed(180.5);
            updateCounty(location.address.county);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Statusanzeige ändern
            // (theoretisch gps abfragen)
            // api abfragen
            // oberfläche aktualisieren
        }
    }
}
