using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;


namespace TachMeiitZomat
{
    class Settings
    {
        private static string REFRESH_INTERVAL_KEY = "refreshInterval";
        private static string DISPLAY_TITLE_KEY = "displayTitle";
        private static string COMPORT_KEY = "comPort";
        private static string COLOR_KEY = "color";
        private static string FONT_KEY = "font";
        private static string FONT_COLOR_KEY = "fontColor";


        Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

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

        public string getRefreshInterval()
        {
            string interval = getSetting(REFRESH_INTERVAL_KEY);
            return interval == "" ? "0" : interval;
        }

        public void setRefreshInterval(string interval)
        {
            AddOrUpdateSetting(REFRESH_INTERVAL_KEY, interval);
        }

        public string getDisplayTitle()
        {
            return getSetting(DISPLAY_TITLE_KEY);
        }

        public void setDisplayTitle(string title)
        {
            AddOrUpdateSetting(DISPLAY_TITLE_KEY, title);
        }

        public string getComPort()
        {
            return getSetting(COMPORT_KEY);
        }

        public void setComPort(string comPort)
        {
            AddOrUpdateSetting(COMPORT_KEY, comPort);
        }

        public string getColor()
        {
            return getSetting(COLOR_KEY);
        }

        public void setColor(int color)
        {
            AddOrUpdateSetting(COLOR_KEY, color.ToString());
        }

        public Font getFont()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
            return (Font) converter.ConvertFromString(getSetting(FONT_KEY));
        }

        public void setFont(Font font)
        {
            TypeConverter Converter = TypeDescriptor.GetConverter(typeof(Font));
            string FontString = Converter.ConvertToString(font);
            AddOrUpdateSetting(FONT_KEY, FontString);
        }

        public Color getFontColor()
        {
            string color = getSetting(FONT_COLOR_KEY);
            return Color.FromArgb(Convert.ToInt32(color));
        }

        public void setFontColor(Color color)
        {
            string colorString = color.ToArgb().ToString(); 
            AddOrUpdateSetting(FONT_COLOR_KEY, colorString);
        }
    }
}
