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

        public ISensorsFactory RandomLocated(int size, int count)
        {
            var random = new Random();
            var sensors = new List<Sensor>();
            for (int i = 0; i < count; i++)
            {
                var x = random.NextDouble() * size;
                var y = random.NextDouble() * size;
                var defaultSensorRange = Settings.Current.Range;
                var defaultBatteryCapacity = Settings.Current.BatteryCapacity;

                var sensor = new Sensor(i, x, y, defaultSensorRange, defaultBatteryCapacity);
                sensor.Battery.Turn(random.NextDouble() < 0.5);
                sensors.Add(sensor);
            }
            this.area.Sensors = sensors;
            return this;
        }

        public ISensorsFactory FromList(IEnumerable<Sensor> sensors)
        {
            area.Sensors = sensors.ToList();
            return this;
        }

        public ISensorsFactory WithRange(double range)
        {
            foreach (var sensor in area.Sensors)
            {
                sensor.Range = range;
            }
            return this;
        }

        public IAreaFactory Then()
        {
            return areaFactory;
        }
    }
}