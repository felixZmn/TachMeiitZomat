using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Drawing;
using System.ComponentModel;

namespace TachMeiitZomat
{
    public partial class SettingsForm : Form
    {
        int color = 0;
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
            Settings settings = new Settings();
            settings.setRefreshInterval(InputRefreshInterval.Value.ToString());
            settings.setDisplayTitle(TbDisplayTitle.Text);
            settings.setComPort(comboBoxPorts.Text);
            settings.setColor(color);
            settings.setFont(font);
            settings.setFontColor(fontColor);
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
            font = settings.getFont();
            fontColor = settings.getFontColor();
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
                btnColor.BackColor = Color.FromArgb(color);
            }   
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            fontDialog1.ShowEffects = true;
            fontDialog1.Font = font;
            fontDialog1.Color = fontColor;
            if(fontDialog1.ShowDialog() == DialogResult.OK)
            {
                font = fontDialog1.Font;
                fontColor = fontDialog1.Color;
            }
        }
    }
}
