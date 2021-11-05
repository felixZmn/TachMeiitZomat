using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;


namespace TachMeiitZomat
{
    public class Settings
    {
        private static string REFRESH_INTERVAL_KEY = "refreshInterval";
        private static string DISPLAY_TITLE_KEY = "displayTitle";
        private static string COMPORT_KEY = "comPort";
        private static string COLOR_KEY = "color";
        private static string FONT_KEY = "font";
        private static string FONT_COLOR_KEY = "fontColor";

        Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public Settings()
        {
            refreshIntervall = Convert.ToInt32(getSetting(REFRESH_INTERVAL_KEY));
            displayTitle = getSetting(DISPLAY_TITLE_KEY);
            comPort = getSetting(COMPORT_KEY);
            color = System.Drawing.Color.FromArgb(Convert.ToInt32(getSetting(COLOR_KEY)));
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
            font = (Font)converter.ConvertFromString(getSetting(FONT_KEY));
            fontColor = System.Drawing.Color.FromArgb(Convert.ToInt32(getSetting(FONT_COLOR_KEY)));
        }

        private int refreshIntervall;

        public int RefreshIntervall
        {
            get { return refreshIntervall; }
            set
            {
                AddOrUpdateSetting(REFRESH_INTERVAL_KEY, value.ToString());
                refreshIntervall = value;
            }
        }

        private string displayTitle;

        public string DisplayTitle
        {
            get { return displayTitle; }
            set
            {
                AddOrUpdateSetting(DISPLAY_TITLE_KEY, value);
                displayTitle = value;
            }
        }

        private string comPort;

        public string COMPort
        {
            get { return comPort; }
            set
            {
                AddOrUpdateSetting(COMPORT_KEY, value);
                comPort = value;
            }
        }

        private Color color;

        public Color Color
        {
            get { return color; }
            set
            {
                AddOrUpdateSetting(COLOR_KEY, value.ToArgb().ToString());
                color = value;
            }
        }

        private Font font;

        public Font Font
        {
            get { return font; }
            set
            {
                TypeConverter Converter = TypeDescriptor.GetConverter(typeof(Font));
                AddOrUpdateSetting(FONT_KEY, Converter.ConvertToString(value));
                font = value;
            }
        }

        private Color fontColor;

        public Color FontColor
        {
            get { return fontColor; }
            set
            {
                AddOrUpdateSetting(FONT_COLOR_KEY, value.ToArgb().ToString());
                fontColor = value;
            }
        }


        private void AddOrUpdateSetting(string key, string value)
        {
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }
            configFile.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        private string getSetting(string key)
        {
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null || settings[key].Value == null)
            {
                return "";
            }
            else
            {
                return settings[key].Value;
            }
        }

    }
}
