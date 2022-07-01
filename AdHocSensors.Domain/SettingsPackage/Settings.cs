using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Domain.SettingsPackage
{
    public class Settings
    {
        public int PoiCount { get; set; } = 121;
        public int SensorCount { get; set; } = 100;
        public double Range { get; set; } = 0.2;
        public double BatteryCapacity { get; set; } = 100;

        public Settings()
        { }

        private static Settings? _defaultInstance;

        public static Settings Default
        {
            get { return _defaultInstance ?? (_defaultInstance = new Settings()); }
        }
    }
}