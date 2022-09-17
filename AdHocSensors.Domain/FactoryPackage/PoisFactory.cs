using AdHocSensors.Domain.SettingsPackage;

namespace AdHocSensors.Domain.FactoryPackage
{
    public class PoisFactory : IPoisFactory
    {
        private readonly Area area;
        private readonly IAreaFactory parent;

        public PoisFactory(Area area, IAreaFactory parent)
        {
            this.area = area;
            this.parent = parent;
        }

        public IPoisFactory EvenlyLocated(int size, int count)
        {
            var poisPerSide = (int)Math.Round(Math.Sqrt(count));
            var distance = (double)size / poisPerSide;
            var pois = new List<Poi>();
            var id = 0;
            for (var y = distance; y < size; y += distance)
            {
                for (var x = distance; x < size; x += distance)
                {
                    pois.Add(new Poi(id++, x, y));
                }
            }
            this.area.Pois = pois;
            return this;
        }

        public IPoisFactory RandomLocated(int size, int count)
        {
            var random = new Random();
            var pois = new List<Poi>();
            for (int i = 0; i < count; i++)
            {
                var x = random.NextDouble() * size;
                var y = random.NextDouble() * size;
                pois.Add(new Poi(i, x, y));
            }
            area.Pois = pois;
            return this;
        }

        public IAreaFactory Then()
        {
            return parent;
        }
    }
}