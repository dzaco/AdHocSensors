using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Domain
{
    public abstract class AreaElementBase
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        protected AreaElementBase(int id, double x, double y)
        {
            Id = id;
            X = x;
            Y = y;
        }
    }
}