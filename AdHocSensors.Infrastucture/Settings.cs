using AdHocSensor.Application.Interfaces;
using AdHocSensors.Infrastucture.Factories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Infrastucture
{
    public class Settings : ISettings
    {
        private int poiCount;
        private Size size;
        private IPoiFactory poiFactory;

        public Settings()
        {
            poiCount = 121;
            size = new Size(100, 100);
            poiFactory = new SquareGridPoiFactory();
        }

        public int Width { get => size.Width; set => size.Height = value; }
        public int Height { get => size.Height; set => size.Height = value; }
        public int PoiCount { get => poiCount; set => poiCount = value; }
        public Size Size { get => size; set => size = value; }
        public IPoiFactory PoiFactory { get => poiFactory; set => poiFactory = value; }
    }
}