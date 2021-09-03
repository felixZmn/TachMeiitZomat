using System;
using System.Windows.Forms;
using System.Configuration;

namespace TachMeiitZomat
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            initForm();
        }

        private void settingsSaveButton_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.setRefreshInterval(InputRefreshInterval.Value.ToString());
            settings.setDisplayTitle(TbDisplayTitle.Text);
            settings.setComPort(TbComPort.Text);
            Close();
        }

        private void initForm()
        {
            Settings settings = new Settings();
            InputRefreshInterval.Value = Convert.ToDecimal(settings.getRefreshInterval());
            TbDisplayTitle.Text = settings.getDisplayTitle();
            TbComPort.Text = settings.getComPort();
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            (Application.OpenForms["Form1"] as Form1).LoadAndApplySettings();
        }
    }
}
