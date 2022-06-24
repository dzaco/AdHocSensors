using AdHocSensor.Application.Interfaces;
using AdHocSensors.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdHocSensors.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoiController : ControllerBase
    {
        private readonly IPoiService poiService;

        public PoiController(IPoiService poiService)
        {
            this.poiService = poiService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Poi>> Get()
        {
            var pois = poiService.GetPois();
            return Ok(pois);
        }

        [HttpGet("{id}")]
        public ActionResult<Poi> Get(int id)
        {
            var poi = poiService.GetPoiWithId(id);
            return Ok(poi);
        }

        [HttpGet("CreatePois")]
        public ActionResult<List<Poi>> CreatePois()
        {
            var res = poiService.CreatePois();
            return Ok(res);
        }
    }
}