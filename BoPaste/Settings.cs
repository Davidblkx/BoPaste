using System.IO;
using MadMilkman.Ini;

namespace BoPaste
{
    public static class Settings
    {
        private static readonly string n = "BoPaste.ini";
        private static IniFile _ini;

        public static void InitializeSettings()
        {
            _ini = new IniFile();

            _ini = !File.Exists(n) ? new IniFile() : OpenFile();

            SetIfNew("Width", "400", "Window");
            SetIfNew("Height", "600", "Window");
            SetIfNew("Left", "20", "Window");
            SetIfNew("Top", "20", "Window");
        }

        private static IniFile OpenFile()
        {
            var file = new IniFile();
            using (var stream = File.OpenRead(n))
            {
                file.Load(stream);
            }
            return file;
        }

        public static void SaveFile()
        {
            using (var stream = File.OpenWrite(n))
            {
                _ini.Save(stream);
            }
        }

        public static void SetSection(string name)
        {
            if (!_ini.Sections.Contains(name))
                _ini.Sections.Add(name);
        }

        public static void Set(string key, string value, string section = "General")
        {
            SetSection(section);
            if (!_ini.Sections[section].Keys.Contains(key))
                _ini.Sections[section].Keys.Add(key, value);
            else
                _ini.Sections[section].Keys[key].Value = value;
        }
        public static void SetIfNew(string key, string value, string section = "General")
        {
            SetSection(section);

            if (!_ini.Sections[section].Keys.Contains(key))
                _ini.Sections[section].Keys.Add(key, value);
        }
        public static string Get(string key, string section = "General")
        {
            SetSection(section);
            try
            {
                return _ini.Sections[section].Keys[key].Value;
            }
            catch
            {
                return "";
            }
        }
    }
}
