using System.Windows.Input;

namespace BoPaste
{
    public class BoPasteItem
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Key InputKey { get; set; }
        public ModifierKeys InputModifiers { get; set; }
    }
}
