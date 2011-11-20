using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArachNGIN.Files.Settings;

namespace Torchiver.TempMover.Settings
{
    class AppSettings:ArachNGIN.Files.Settings.JSONSettings1<AppSettings>
    {
        public string ScanPath = String.Empty;
    }
}
