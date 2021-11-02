using System;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

/*
 * Zeit:
 * 2021.08.30: 1500 - 1730 -> 2.5h
 * 2021.09.02: 1300 - 1500 -> 2h
 * 2021.09.02: 1900 - 2100 -> 2h
 * 2021.09.03: 1500 - 1700 -> 2h
 */

/*
 * ToDo: ThreadAbortException vermeiden, wenn man einfach die einstellungen öffnet, während der thread im hintergrund läuft
 */
namespace TachMeiitZomat
{
    public partial class Form1 : Form
    {
        public static ManualResetEvent mre = new ManualResetEvent(true);

        GPSHandler gps;
        Thread thread;

        public Form1()
        {
            InitializeComponent();
            LoadAndApplySettings();
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

        /// <summary>
        /// Load and Apply settings from settings file
        /// </summary>
        public void LoadAndApplySettings()
        {
            Settings settings = new Settings();
            this.Text = settings.getDisplayTitle();
            this.BackColor = Color.FromArgb(Convert.ToInt32(settings.getColor()));
            // times 1000, because interval is saved in seconds, not in ms
            gpsLocationTimer.Interval = Convert.ToInt32(settings.getRefreshInterval()) * 1000;
            if (thread != null && thread.IsAlive)
            {
                thread.Abort();
            }
            if(gps!=null)
            {
                gps.Dispose();
            }
            gps = new GPSHandler(settings.getComPort());
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
            System.Diagnostics.Process.Start("mailto:" + "example@example.com");
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
