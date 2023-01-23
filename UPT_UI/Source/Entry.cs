using System;

namespace UPT_UI
{
    public class Entry
    {
        [STAThread]
        public static void Main(string[] args) 
        {
            if (args != null && args.Length > 0) {
                // ...
            } else {
                app.Run();
            }                var app = new App();
                app.InitializeComponent();

        }
    }
}