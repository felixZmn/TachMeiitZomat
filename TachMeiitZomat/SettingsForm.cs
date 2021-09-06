using System;
using System.Windows.Forms;
using System.Configuration;
using System.IO.Ports;
using System.Drawing;

namespace TachMeiitZomat
{
    public partial class SettingsForm : Form
    {
        int color = 0;
        public SettingsForm()
        {
            InitializeComponent();
            var ports = SerialPort.GetPortNames();
            foreach (var port in ports)
            {
                comboBoxPorts.Items.Add(port);
            }
            initForm();
        }

        private void settingsSaveButton_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.setRefreshInterval(InputRefreshInterval.Value.ToString());
            settings.setDisplayTitle(TbDisplayTitle.Text);
            settings.setComPort(comboBoxPorts.Text);
            settings.setColor(color);
            Close();
        }

        private void initForm()
        {
            Settings settings = new Settings();
            InputRefreshInterval.Value = Convert.ToDecimal(settings.getRefreshInterval());
            TbDisplayTitle.Text = settings.getDisplayTitle();
            comboBoxPorts.Text = settings.getComPort();
            color = Convert.ToInt32(settings.getColor()); 
            btnColor.BackColor = Color.FromArgb(color);
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            (Application.OpenForms["Form1"] as Form1).LoadAndApplySettings();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                color = colorDialog1.Color.ToArgb();
                Settings settings = new Settings();
                btnColor.BackColor = Color.FromArgb(color);
            }   
        }
    }
}
