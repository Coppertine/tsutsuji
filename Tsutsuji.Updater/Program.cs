using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Tsutsuji.Updater.Screens;

namespace Tsutsuji.Updater
{
    static class Program
    {
        public static string HyperfluxPath = @"C:\Program Files (x86)\Steam\steamapps\common\Geometry Dash\_hyperflux";
        public static string HyperfluxResourcesPath = HyperfluxPath + @"\Resources";
        public static string GeometryDashPath = @"C:\Program Files (x86)\Steam\steamapps\common\Geometry Dash";


        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length < 1 || args[0] != "--launcher")
                ErrorThrowMessage("You cannot properly run the updater executable by itself.");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (!Directory.Exists(GeometryDashPath)) 
                ErrorThrowMessage("You do not have Geometry Dash installed on this PC!");

            if (!Directory.Exists(HyperfluxPath))
            {
                Debug.WriteLine("First time user installation.");
                Directory.CreateDirectory(HyperfluxPath);
                Directory.CreateDirectory(HyperfluxResourcesPath);

                Application.Run(new Updater(UpdaterType.First));
                return;
            }
            
            try
            {
                Application.Run(new Updater(UpdaterType.Update));
            }
             catch (Exception e)
            {
                Debug.WriteLine(e.ToString);
                return;
             }
        }
        
        private void ErrorThrowMessage(string msg) 
            => if (MessageBox.Show(msg, "Tsutsuji Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK) Application.Exit();
    }
}
