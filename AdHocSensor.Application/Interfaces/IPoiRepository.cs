using AdHocSensors.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensor.Application.Interfaces
{
    public interface IPoiRepository
    {
        void Add(Poi poi);

        void Add(IEnumerable<Poi> pois);

        List<Poi> GetPois();

        Poi GetPoiWithId(int id);

        void DeleteAll();
    }
}