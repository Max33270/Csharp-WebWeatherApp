using System;
using Gtk;
using MeteoProject;

namespace MeteoProject
{

    class Program 
    {   

        [STAThread]
        public static void Main(string[] args)
        {   
            Application.Init();
            var app = new Application("org.MeteoAPI.MeteoAPI", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);
            var win = new Glade_meteo();
            app.AddWindow(win);
            win.Show();
            Application.Run();
        }

        
    }
}
