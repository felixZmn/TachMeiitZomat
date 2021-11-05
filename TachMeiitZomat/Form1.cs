using System;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

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
        public static ManualResetEvent mre = new ManualResetEvent(true);
        public static Settings Settings = new Settings();

        GPSHandler gps;
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
            // 
            if (gps.getReady())
            {
                statusDisplay.Text = StatusEnum.STATUS_RUNNING;
                if (thread.ThreadState == ThreadState.Unstarted)
                {
                    thread.Start();
                }
                else
                {
                    mre.Set();
                }
                gpsLocationTimer.Enabled = true;
                speedTimer.Enabled = true;
            } else
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
            mre.Reset();
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
            updateCounty(gps.getCountyOrCity());
        }

        /// <summary>
        /// periodic update of displayed speed  <br />
        /// Source of speed is the gps receiver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void speedTimer_Tick(object sender, EventArgs e)
        {
            updateSpeed(gps.getSpeed());
        }

        private void LoadSettings()
        {
            // einstellungsdatei lesen, parsen und in settings-objekt zur verfügung stellen
            Settings = new Settings();
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

            Foo();
        }

        /// <summary>
        /// Load and Apply settings from settings file
        /// </summary>
        public void Foo()
        {
            
            
            if (thread != null && thread.IsAlive)
            {
                thread.Abort();
            }
            if(gps!=null)
            {
                gps.Dispose();
            }
            gps = new GPSHandler(Settings.COMPort);
            thread = new Thread(new ThreadStart(gps.ReadGpsSensor));
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
            // ToDo: Echte Mail-Adresse dazu schreiben
            System.Diagnostics.Process.Start("mailto:" + "felix.zimmermann.1.de@gmail.com");
        }

        private void überToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
        }
    }
}
