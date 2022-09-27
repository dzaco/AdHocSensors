using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Domain.SettingsPackage
{
    public class Settings : IEquatable<Settings>
    {
        public int AreaSize { get; set; } = 100;
        public int PoiCount { get; set; } = 121;
        public int SensorCount { get; set; } = 5;
        public double Range { get; set; } = 20;
        public double BatteryCapacity { get; set; } = 100;
        public double Scale { get; set; } = 7;

        public Area Area { get; set; }

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

        public void EmitSizeChanged()
        {
            ScaleChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Scale)));
        }

        public void CopyFrom(Settings source)
        {
            this.PoiCount = source.PoiCount;
            this.SensorCount = source.SensorCount;
            this.Range = source.Range;
            this.BatteryCapacity = source.BatteryCapacity;
            this.Scale = source.Scale;
            this.AreaSize = source.AreaSize;
        }

        public void Reset()
        {
            CopyFrom(Default);
        }

        public bool Equals(Settings? other)
        {
            if (other is null) return false;
            return
                other.Scale == this.Scale &&
                other.Range == this.Range &&
                other.PoiCount == this.PoiCount &&
                other.AreaSize == this.AreaSize &&
                other.SensorCount == this.SensorCount;
        }

        private static Settings? _currentInstance;
        private static Settings? _defaultInstance;
        private static Settings? _editorInstance;

        public static Settings Current => _currentInstance ?? (_currentInstance = new Settings());
        public static Settings Default => _defaultInstance ?? (_defaultInstance = new Settings());
        public static Settings Editor => _editorInstance ?? (_editorInstance = new Settings(_currentInstance));
    }
}