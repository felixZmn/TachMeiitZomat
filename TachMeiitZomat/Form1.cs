using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using System.Device.Location; // Used for GPS Stuff... hopefully

/*
 * Zeit:
 * 2021.08.30: 1500 - 1730 -> 2.5h
 */
namespace TachMeiitZomat
{
    public partial class Form1 : Form
    {
        HttpClient client = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            statusDisplay.Text = StatusEnum.STATUS_STOPPED;
            LoadAndApplySettings();
            updateSpeed(0);
            updateCounty("");
            // Referer für OpenStreetMap Location Zeug. Kann weg, wenn die Windows API taugt
            client.DefaultRequestHeaders.Add("Referer", "TachMeiitZomat");
        }

        /// <summary>
        /// Start all timer <br />
        /// First determination of speed and county
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusDisplay.Text = StatusEnum.STATUS_RUNNING;
            gpsLocationTimer.Enabled = true;
            speedTimer.Enabled = true;

            // ToDo: Replace with correct logic
            var location = getLocation("48.716669", "10.434100");
            updateCounty(location.address.county);
        }

        /// <summary>
        /// Stop all timers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gpsLocationTimer.Enabled = false;
            speedTimer.Enabled = false;
            statusDisplay.Text = StatusEnum.STATUS_STOPPED;
        }

        /// <summary>
        /// Open settings window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show();
        }

        /// <summary>
        /// Periodic update of the displayed county <br />
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gpsLocationTimer_Tick(object sender, EventArgs e)
        {
            // Statusanzeige ändern
            // (theoretisch gps abfragen)
            // api abfragen
            // oberfläche aktualisieren
        }

        /// <summary>
        /// periodic update of displayed speed  <br />
        /// Source of speed is the gps receiver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void speedTimer_Tick(object sender, EventArgs e)
        {
            updateSpeed(getSpeed());
        }

        /// <summary>
        /// Load and Apply settings from settings file
        /// </summary>
        public void LoadAndApplySettings()
        {
            Settings settings = new Settings();
            this.Text = settings.getDisplayTitle();
            // times 1000, because interval is saved in seconds, not in ms
            gpsLocationTimer.Interval = Convert.ToInt32(settings.getRefreshInterval()) * 1000;
        }

        /// <summary>
        /// update speed label
        /// </summary>
        /// <param name="speed">displayed speed</param>
        private void updateSpeed(double speed)
        {
            labelSpeed.Text = speed.ToString() + " km/h";
        }

        /// <summary>
        /// update county label
        /// </summary>
        /// <param name="county">displayed county</param>
        private void updateCounty(String county)
        {
            labelCounty.Text = county;
        }

        /*
         * get speed from gps receiver
         * ToDo: Move to own GPS class
         */
        private int getSpeed()
        {
            return 7;
        }

        /*
         * get location from gps receiver
         * ToDo: Move to own GPS class
         */
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
    }
}
