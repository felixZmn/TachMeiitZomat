using System;
using System.Configuration;


namespace TachMeiitZomat
{
    class Settings
    {
        private static string REFRESH_INTERVAL_KEY = "refreshInterval";
        private static string DISPLAY_TITLE_KEY = "displayTitle";
        private static string COMPORT_KEY = "comPort";

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
    }
}
