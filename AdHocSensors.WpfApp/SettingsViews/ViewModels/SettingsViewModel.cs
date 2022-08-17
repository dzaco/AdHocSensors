using AdHocSensors.Domain.SettingsPackage;
using AdHocSensors.WpfApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.WpfApp.SettingsViews.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public Settings Settings { get; set; }
        public List<int> PossiblePoiCount { get; set; }

        public SettingsViewModel()
        {
            Settings = Settings.Editor;
            PossiblePoiCount = new List<int>() { 121, 225, 441 };
        }
    }
}