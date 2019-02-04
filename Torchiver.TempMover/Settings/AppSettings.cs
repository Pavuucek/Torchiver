using System.Windows.Forms;
using ArachNGIN.Files.Settings;

namespace Torchiver.TempMover.Settings
{
    internal class AppSettings : JsonSettings1<AppSettings>
    {
        public aInfo AppInfo = new aInfo();
        public int Interval = 1;
        public string MovePath = "T:\\";
        public string ScanPath = string.Empty;

        public class aInfo
        {
            public string AppCompany = Application.CompanyName;
            public string AppName = Application.ProductName;
            public string AppVersion = Application.ProductVersion;
        }
    }
}