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
        public Size Size { get; }

        public Area(Size size)
        {
            this.Size = size;
        }
    }
}