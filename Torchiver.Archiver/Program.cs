using System;
using System.Data.Entity;
//using System.Data.EntityClient;
using System.Windows.Forms;
using ArachNGIN.Components.Console;
using ArachNGIN.Components.Console.Misc;
using Torchiver.Archiver.DBModel;
using Torchiver.Archiver.Forms;

namespace Torchiver.Archiver
{
    static class Program
    {
        public static MainForm Mainform;

        public static DataContext Data=new DataContext();

#if DEBUG
        public static DebugConsole Konzole;
#endif
        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if DEBUG
            Konzole = new DebugConsole
            {
                AutoSave = ConsoleAutoSave.OnLineAdd,
                Caption = "Konzole",
                EchoCommands = true,
                ScreenLocation = ConsoleLocation.TopRight,
                ProcessInternalCommands = true,
            };
            Konzole.Show();
#endif
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
            Data.Database.CreateIfNotExists();
            Application.Run(Mainform = new MainForm());
        }
    }
}
