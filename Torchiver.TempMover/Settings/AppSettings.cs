using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArachNGIN.Files.Settings;
using System.Windows.Forms;

namespace Torchiver.TempMover.Settings
{
    class AppSettings:ArachNGIN.Files.Settings.JsonSettings1<AppSettings>
    {
        public class aInfo
        {
            public string AppName = Application.ProductName;
            public string AppCompany = Application.CompanyName;
            public string AppVersion = Application.ProductVersion;
        }
        public aInfo AppInfo = new aInfo();
        public string ScanPath = String.Empty;
        public string MovePath = "T:\\";
        public int Interval = 1;
    }
}
