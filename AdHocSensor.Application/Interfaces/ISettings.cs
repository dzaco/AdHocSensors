using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensor.Application.Interfaces
{
    public interface ISettings
    {
        int Width { get; set; }
        int Height { get; set; }
        int PoiCount { get; set; }
        Size Size { get; set; }
        IPoiFactory PoiFactory { get; set; }
    }
}