using AdHocSensor.Application.Interfaces;
using AdHocSensors.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Infrastucture.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private static Area _area = new Area(new Size(100, 100));

        public Area GetArea()
        {
            return _area;
        }
    }
}