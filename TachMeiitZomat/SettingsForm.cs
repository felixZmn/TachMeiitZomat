using System;
using System.Windows.Forms;
using System.Configuration;
using System.IO.Ports;

namespace TachMeiitZomat
{
    public partial class SettingsForm : Form
    {
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
            Close();
        }

        private void initForm()
        {
            Settings settings = new Settings();
            InputRefreshInterval.Value = Convert.ToDecimal(settings.getRefreshInterval());
            TbDisplayTitle.Text = settings.getDisplayTitle();
            comboBoxPorts.Text = settings.getComPort();
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            (Application.OpenForms["Form1"] as Form1).LoadAndApplySettings();
        }
    }
}
