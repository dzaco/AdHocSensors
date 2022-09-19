using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Domain
{
    public class Area
    {
        public List<Poi> Pois { get; set; }
        public List<Sensor> Sensors { get; set; }

        public Area()
        {
            this.Pois = new List<Poi>();
            this.Sensors = new List<Sensor>();
        }

        public void DetectCoveredPois()
        {
            foreach (var poi in Pois)
            {
                poi.IsCovered = false;
                foreach (var sensor in Sensors)
                {
                    if (sensor.IsInRange(poi))
                    {
                        sensor.Pois.Add(poi);
                        if (sensor.Battery.IsOn)
                            poi.IsCovered = true;
                    }
                }
            }
        }
    }
}