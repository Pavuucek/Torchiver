using System;
using System.Windows.Forms;
using ArachNGIN.Files.TempDir;
using Torchiver.TempMover.Forms;
using Torchiver.TempMover.Settings;

namespace Torchiver.TempMover
{
    internal static class Program
    {
        public static MainForm MF;
        public static TempManager TempM = new TempManager();
        public static AppSettings aSettings = AppSettings.Load("config.json");

        /// <summary>
        ///     Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += Application_ApplicationExit;
            Application.Run(MF = new MainForm());
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
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