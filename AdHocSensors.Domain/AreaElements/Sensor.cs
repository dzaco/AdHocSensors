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

        public Sensor(int id, double x, double y, double range, double batteryCapacity) : base(id, x, y)
        {
            Range = range;
            Battery = new Battery(batteryCapacity);
        }
    }
}