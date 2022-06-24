using AdHocSensor.Application.Interfaces;
using AdHocSensors.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Infrastucture.Repositories
{
    public class PoiRepository : IPoiRepository
    {
        public static List<Poi> _pois = new List<Poi>();

        // Get
        public List<Poi> GetPois()
        {
            return _pois;
        }

        public Poi GetPoiWithId(int id)
        {
            return _pois.FirstOrDefault(poi => poi.Id == id);
        }

        // Add
        public void Add(Poi poi)
        {
            _pois.Add(poi);
        }

        public void Add(IEnumerable<Poi> pois)
        {
            _pois.AddRange(pois);
        }

        public void DeleteAll()
        {
            _pois = new List<Poi>();
        }
    }
}