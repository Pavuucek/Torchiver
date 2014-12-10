using System;
using System.Data.Entity;
//using System.Data.EntityClient;
using System.Windows.Forms;
using Torchiver.Archiver.DBModel;
using Torchiver.Archiver.Forms;

namespace Torchiver.Archiver
{
    static class Program
    {
        public static MainForm Mainform;

        public static DataContext Data=new DataContext();

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
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
            Data.Database.CreateIfNotExists();
            Application.Run(Mainform = new MainForm());
        }
    }
}
