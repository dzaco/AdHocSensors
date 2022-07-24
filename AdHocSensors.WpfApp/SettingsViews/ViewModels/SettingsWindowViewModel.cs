using AdHocSensors.Domain.SettingsPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.WpfApp.SettingsViews.ViewModels
{
    public class SettingsWindowViewModel
    {
        public Settings Settings { get; set; }

        public SettingsWindowViewModel()
        {
            Settings = Settings.Default;
        }
    }
}