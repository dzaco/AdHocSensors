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
        public double Scale { get; set; } = 100;

        private Settings()
        { }

        private Settings(Settings source)
        {
            if (source is null)
                CopyFrom(Default);
            else
                CopyFrom(source);
        }

        public void CopyFrom(Settings source)
        {
            this.PoiCount = source.PoiCount;
            this.SensorCount = source.SensorCount;
            this.Range = source.Range;
            this.BatteryCapacity = source.BatteryCapacity;
            this.Scale = source.Scale;
        }

        public void Reset()
        {
            CopyFrom(Default);
        }

        private static Settings? _currentInstance;
        private static Settings? _defaultInstance;
        private static Settings? _editorInstance;

        public static Settings Current => _currentInstance ?? (_currentInstance = new Settings());
        public static Settings Default => _defaultInstance ?? (_defaultInstance = new Settings());
        public static Settings Editor => _editorInstance ?? (_editorInstance = new Settings(_currentInstance));
    }
}