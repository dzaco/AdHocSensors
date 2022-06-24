using AdHocSensor.Application.Interfaces;
using AdHocSensors.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Infrastucture.Factories
{
    public class SquareGridPoiFactory : IPoiFactory
    {
        public List<Poi> Create(Size areaSize, int poiCount)
        {
            var pois = new List<Poi>();
            var distance = Math.Sqrt((areaSize.Width * areaSize.Height) / (poiCount));
            var index = 1;

            for (var y = 0.0; y <= areaSize.Height; y += distance)
            {
                for (var x = 0.0; x <= areaSize.Width; x += distance)
                {
                    pois.Add(new Poi(index++, x, y));
                }
            }

            return pois;
        }
    }
}