﻿using AdHocSensors.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensor.Application.Interfaces
{
    public interface IPoiFactory
    {
        List<Poi> Create(Size areaSize, int poiCount);
    }
}