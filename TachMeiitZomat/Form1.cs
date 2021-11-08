using System;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;
using System.Threading.Tasks;

/*
 * ToDo: ThreadAbortException vermeiden, wenn man einfach die Einstellungen öffnet, während der Thread im hintergrund läuft
 * ToDo: HttpClient auf WebClient umstellen
 * ToDo: Tread beim klick auf start starten
 * ToDo: Foo() Fixen
 */
namespace TachMeiitZomat
{
    public partial class Form1 : Form
    {
        public static Settings Settings = new Settings();

        GpsSensor sensor;
        Thread thread;

        public Form1()
        {
            InitializeComponent();
            ApplySettings();
            statusDisplay.Text = StatusEnum.STATUS_STOPPED;
        }

        /// <summary>
        /// Start all timer <br />
        /// First determination of speed and county
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PrepareBackgroundTasks();
                statusDisplay.Text = StatusEnum.STATUS_RUNNING;
                thread.Start();
                gpsLocationTimer.Enabled = true;
                speedTimer.Enabled = true;
                startToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("GPS-Empfänger konnte nicht initialisiert werden. Bitte Einstellungen und Verbindung prüfen");
            }
        }

        /// <summary>
        /// Stop all timers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopBackgroundTasks();
        }

        /// <summary>
        /// Open settings window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopBackgroundTasks();
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
            var cords = sensor.GetCoordinate();
            try{
                var result = Task.Run(() => CoordinateResolver.Resolve(cords)).Result;
                var location = JsonConvert.DeserializeObject<OpenStreetMapLocation>(result);
                if (location.address.county != null)
                {
                    updateCounty(location.address.county);
                }
                else if (location.address.city != null)
                {
                    updateCounty(location.address.city);
                }
                else
                {
                    updateCounty("unbekannt");
                }
            } catch (Exception ex)
            {
                updateCounty("Netzwerkfehler");
            }
        }



        /// <summary>
        /// periodic update of displayed speed  <br />
        /// Source of speed is the gps receiver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void speedTimer_Tick(object sender, EventArgs e)
        {
            updateSpeed(sensor.GetCoordinate().Speed);
        }

        public void ApplySettings()
        {
            this.Text = Settings.DisplayTitle;
            this.BackColor = Settings.Color;
            labelSpeed.ForeColor = Settings.FontColor;
            labelSpeed.Font = Settings.Font;
            labelCounty.ForeColor = Settings.FontColor;
            labelCounty.Font = Settings.Font;
            // times 1000, because interval is saved in seconds, not in ms
            gpsLocationTimer.Interval = Settings.RefreshIntervall * 1000;
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

        private void kontaktToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:" + "felix.zimmermann.1.de@gmail.com");
        }

        private void überToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
            }
        }
        private void PrepareBackgroundTasks()
        {
            sensor = new GpsSensor(Settings.COMPort);
            thread = new Thread(new ThreadStart(sensor.ReadLoop));
        }
        private void StopBackgroundTasks()
        {
            gpsLocationTimer.Enabled = false;
            speedTimer.Enabled = false;
            statusDisplay.Text = StatusEnum.STATUS_STOPPED;
            startToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = false;
            if (sensor != null)
            {
                sensor.Dispose();
            }
            if (thread != null)
            {
                thread.Abort();
            }
        }
    }
}
