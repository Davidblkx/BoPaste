using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using NHotkey;
using NHotkey.Wpf;
using Clipboard = System.Windows.Clipboard;

namespace BoPaste
{
    public class Manager
    {
        private const string Path = "hotkeys.json";
        public Dictionary<string, BoPasteItem> Collection { get; set; }

        public Manager()
        {
            if (!File.Exists(Path))
                File.Create(Path);

            var source = File.ReadAllText(Path);
            Collection = string.IsNullOrEmpty(source)
                ? new Dictionary<string, BoPasteItem>()
                : JsonConvert.DeserializeObject<Dictionary<string, BoPasteItem>>(source);
        }

        public void StartHotkeys()
        {
            foreach (var itm in Collection)
            {
                HotkeyManager.Current.Remove(itm.Value.Name);
                HotkeyManager.Current.AddOrReplace(itm.Value.Name, itm.Value.InputKey, itm.Value.InputModifiers,
                    (object sender, HotkeyEventArgs e) =>
                    {
                        Clipboard.SetText(itm.Value.Text);

                        SendKeys.SendWait("^v");

                        e.Handled = true;
                    });
            }
        }

        public void Save()
        {
            File.WriteAllText(Path, JsonConvert.SerializeObject(Collection));
        }
    }
}
