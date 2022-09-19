using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Domain
{
    public class Sensor : AreaElementBase
    {
        public Battery Battery { get; set; }
        public List<Poi> Pois { get; set; }

        public Sensor(int id, double x, double y, double range, double batteryCapacity) : base(id, x, y)
        {
            Range = range;
            Battery = new Battery(batteryCapacity);
            Pois = new List<Poi>();
        }

        public double Coverage
        {
            get
            {
                if (Battery.IsOn)
                    return 1;
                else
                    return (double)Pois.Where(poi => poi.IsCovered).Count() / Pois.Count;
            }
        }

        public bool IsInRange(Poi poi)
        {
            return DistanceTo(poi) <= Range;
        }
    }
}