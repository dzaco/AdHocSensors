using AdHocSensors.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensor.Application.Interfaces
{
    public interface IPoiService
    {
        List<Poi> GetPois();

        List<Poi> CreatePois();

        Poi GetPoiWithId(int id);
    }
}