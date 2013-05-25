using System;
using System.Windows.Forms;
using Torchiver.Archiver.DBModel;
using Torchiver.Archiver.Forms;

namespace Torchiver.Archiver
{
    static class Program
    {
        public static MainForm Mainform;

        public static DataContext Data = new DataContext();//("providername=System.Data.SQLite;provider connection string='data source=.\\OurDatabase.db'");

        //public static DebugConsole Konzole;

        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*Konzole = new DebugConsole
            {
                AutoSave = ConsoleAutoSave.OnLineAdd,
                Caption = "Konzole",
                EchoCommands = true,
                ScreenLocation = ConsoleLocation.TopRight,
                ProcessInternalCommands = true,
                UsePlainView = true
            };*/
            Application.Run(Mainform = new MainForm());
        }
    }
}
