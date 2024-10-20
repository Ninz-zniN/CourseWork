using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Resources.ClassLb
{
    public class Note
    {
        public string Header { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Color HeaderColor { get; set; } = Colors.Transparent;
        public string Group { get; set; } = string.Empty;
        public Note(string Description)
        {
            this.Description = Description;
        }
        public Note(string Header, string Description, Color HeaderColor, string Group)
        {
            this.Header = Header;
            this.Description = Description;
            this.HeaderColor = HeaderColor;
            this.Group = Group;
        }
    }
}
