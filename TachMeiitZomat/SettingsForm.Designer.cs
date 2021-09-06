namespace TachMeiitZomat
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.settingsSaveButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TbDisplayTitle = new System.Windows.Forms.TextBox();
            this.InputRefreshInterval = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.InputRefreshInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Aktualisierungsintervall";
            // 
            // settingsSaveButton
            // 
            this.settingsSaveButton.Location = new System.Drawing.Point(16, 135);
            this.settingsSaveButton.Name = "settingsSaveButton";
            this.settingsSaveButton.Size = new System.Drawing.Size(299, 23);
            this.settingsSaveButton.TabIndex = 1;
            this.settingsSaveButton.Text = "Speichern";
            this.settingsSaveButton.UseVisualStyleBackColor = true;
            this.settingsSaveButton.Click += new System.EventHandler(this.settingsSaveButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(291, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "sec";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Titel";
            // 
            // TbDisplayTitle
            // 
            this.TbDisplayTitle.Location = new System.Drawing.Point(165, 37);
            this.TbDisplayTitle.Name = "TbDisplayTitle";
            this.TbDisplayTitle.Size = new System.Drawing.Size(120, 20);
            this.TbDisplayTitle.TabIndex = 5;
            // 
            // InputRefreshInterval
            // 
            this.InputRefreshInterval.Location = new System.Drawing.Point(165, 11);
            this.InputRefreshInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.InputRefreshInterval.Name = "InputRefreshInterval";
            this.InputRefreshInterval.Size = new System.Drawing.Size(120, 20);
            this.InputRefreshInterval.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "COM-Port";
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(164, 66);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPorts.TabIndex = 9;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 193);
            this.Controls.Add(this.comboBoxPorts);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.InputRefreshInterval);
            this.Controls.Add(this.TbDisplayTitle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.settingsSaveButton);
            this.Controls.Add(this.label1);
            this.Name = "SettingsForm";
            this.Text = "Einstellungen";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.InputRefreshInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button settingsSaveButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TbDisplayTitle;
        private System.Windows.Forms.NumericUpDown InputRefreshInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxPorts;
    }
}