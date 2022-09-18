using AdHocSensors.Domain.SettingsPackage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Domain.FactoryPackage
{
    public class AreaFactory : IAreaFactory
    {
        private Area _area;

        public AreaFactory()
        {
            this._area = new Area();
        }

        public AreaFactory(Area area)
        {
            if (area is not null)
                _area = area;
            else
                _area = new Area();
        }

        public Area Build()
        {
            _area.DetectCoveredPois();
            return _area;
        }

        public IPoisFactory WithPois()
        {
            return new PoisFactory(_area, this);
        }

        public ISensorsFactory WithSensors()
        {
            return new SensorsFactory(_area, this);
        }
    }
}