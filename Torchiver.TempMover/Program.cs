using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Torchiver.TempMover.Forms;
using Torchiver.TempMover.Settings;
using ArachNGIN.Files;
using ArachNGIN.Files.TempDir;

namespace Torchiver.TempMover
{
    static class Program
    {
        public static MainForm MF;
        public static TempManager TempM = new TempManager();
        public static AppSettings aSettings = AppSettings.Load("config.json");
        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            Application.Run(MF = new MainForm());
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                aSettings.Save("config.json");
            }
            catch
            {

            }
        }
    }
}
