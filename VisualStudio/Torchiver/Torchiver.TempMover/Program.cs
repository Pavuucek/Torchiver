using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Torchiver.TempMover.Forms;
using ArachNGIN.Files;
namespace Torchiver.TempMover
{
    static class Program
    {
        public static MainForm MF;
        public static TempManager TempM;
        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(MF = new MainForm());
        }
    }
}
