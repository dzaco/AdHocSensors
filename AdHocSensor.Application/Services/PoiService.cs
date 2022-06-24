using AdHocSensor.Application.Interfaces;
using AdHocSensors.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensor.Application.Services
{
    public class PoiService : IPoiService
    {
        private readonly IPoiRepository _poiRepository;
        private readonly ISettings _settings;

        public PoiService(IPoiRepository repository, ISettings settings)
        {
            _poiRepository = repository;
            _settings = settings;
        }

        public List<Poi> CreatePois()
        {
            IPoiFactory factory = _settings.PoiFactory;
            var created = factory.Create(_settings.Size, _settings.PoiCount);
            if (created.Any())
            {
                _poiRepository.DeleteAll();
                _poiRepository.Add(created);
            }

            return this.GetPois();
        }

        public List<Poi> GetPois()
        {
            return this._poiRepository.GetPois();
        }

        public Poi GetPoiWithId(int id)
        {
            return this._poiRepository.GetPoiWithId(id);
        }
    }
}