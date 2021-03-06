using AdHocSensors.Domain.SettingsPackage;

namespace AdHocSensors.Domain.FactoryPackage
{
    internal class SensorsFactory : ISensorsFactory
    {
        private Area area;
        private AreaFactory areaFactory;

        public SensorsFactory(Area area, AreaFactory areaFactory)
        {
            this.area = area;
            this.areaFactory = areaFactory;
        }

        public ISensorsFactory RandomLocated(int count)
        {
            var random = new Random();
            var sensors = new List<Sensor>();
            for (int i = 0; i < count; i++)
            {
                var x = random.NextDouble();
                var y = random.NextDouble();
                var defaultSensorRange = Settings.Current.Range;
                var defaultBatteryCapacity = Settings.Current.BatteryCapacity;

                sensors.Add(new Sensor(i, x, y, defaultSensorRange, defaultBatteryCapacity));
            }
            this.area.Sensors = sensors;
            return this;
        }

        public IAreaFactory Then()
        {
            return areaFactory;
        }
    }
}