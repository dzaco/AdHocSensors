using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Domain.SettingsPackage
{
    public class Settings
    {
        public int AreaSize { get; set; } = 100;
        public int PoiCount { get; set; } = 121;
        public int SensorCount { get; set; } = 5;
        public double Range { get; set; } = 20;
        public double BatteryCapacity { get; set; } = 100;
        public double Scale { get; set; } = 7;

        private Settings()
        { }

        private Settings(Settings source)
        {
            if (source is null)
                CopyFrom(Default);
            else
                CopyFrom(source);
        }

        public event EventHandler ScaleChanged;

        public void EmitScaleChanged()
        {
            ScaleChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Scale)));
        }

        public void CopyFrom(Settings source)
        {
            this.PoiCount = source.PoiCount;
            this.SensorCount = source.SensorCount;
            this.Range = source.Range;
            this.BatteryCapacity = source.BatteryCapacity;

            var scaleBeforeChanged = Scale;
            this.Scale = source.Scale;
            if (scaleBeforeChanged != Scale)
                EmitScaleChanged();
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