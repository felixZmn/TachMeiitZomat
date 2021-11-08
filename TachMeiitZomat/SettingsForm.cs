using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Drawing;

namespace TachMeiitZomat
{
    public partial class SettingsForm : Form
    {
        Color color;
        Font font;
        Color fontColor;

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
            Form1.Settings.RefreshIntervall = Convert.ToInt32(InputRefreshInterval.Value);
            Form1.Settings.DisplayTitle = TbDisplayTitle.Text;
            Form1.Settings.COMPort = comboBoxPorts.Text;
            Form1.Settings.Color = color;
            Form1.Settings.Font = font;
            Form1.Settings.FontColor = fontColor;
            Close();
        }

        private void initForm()
        {
            InputRefreshInterval.Value = Convert.ToDecimal(Form1.Settings.RefreshIntervall);
            TbDisplayTitle.Text = Form1.Settings.DisplayTitle;
            comboBoxPorts.Text = Form1.Settings.COMPort;
            color = Form1.Settings.Color;
            btnColor.BackColor = Form1.Settings.Color;
            font = Form1.Settings.Font;
            fontColor = Form1.Settings.FontColor;
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            (Application.OpenForms["Form1"] as Form1).ApplySettings();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                color = colorDialog1.Color;
                btnColor.BackColor = colorDialog1.Color;
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            fontDialog1.ShowEffects = true;
            fontDialog1.Font = font;
            fontDialog1.Color = fontColor;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                font = fontDialog1.Font;
                fontColor = fontDialog1.Color;
            }
        }
    }
}
