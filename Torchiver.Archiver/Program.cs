using System;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Windows.Forms;
using Torchiver.Archiver.DBModel;
using Torchiver.Archiver.Forms;

namespace Torchiver.Archiver
{
    static class Program
    {
        public static MainForm Mainform;

        public static DataContext Data;// = new DataContext(@"metadata=res://*/DBModel.DataContext.csdl|res://*/DBModel.DataContext.ssdl|res://*/DBModel.DataContext.msl;providerName=System.Data.SQLite;provider connection string='data source=.\\OurDatabase.db';");
        //metadata=res://*/DataContext.csdl|res://*/DataContext.ssdl|res://*/DataContext.msl;

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
            //Database.DefaultConnectionFactory = new SQLiteConnectionFactory();
            var ecb = new EntityConnectionStringBuilder();
            // this for some weird reason doesn't work. IDK why.
            //ecb.Metadata = @"res://*/DBModel.DataContext.csdl|res://*/DBModel.DataContext.ssdl|res://*/DBModel.DataContext.msl;";
            // and why DAFUQ does this work?
            //ecb.Metadata = @"res://*/";
            ecb.Provider = "System.Data.SQLite";
            //ecb.ProviderConnectionString = @"data source=a.db;Version=3;New=false;";
            //MessageBox.Show(ecb.ToString());
            Data=new DataContext(ecb.ToString());
            
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
            //Data.Database.CreateIfNotExists();
            Application.Run(Mainform = new MainForm());
        }
    }
}
