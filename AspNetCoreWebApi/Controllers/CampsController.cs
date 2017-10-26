using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AspNetCoreWebApi.ActionFilters;
using AspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class CampsController : Controller
    {
        private CompContext _campCompContext;
        private ILogger<CampsController> _logger;

        public CampsController(CompContext campCompContext, ILogger<CampsController> logger)
        {
            _campCompContext = campCompContext;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var camps = _campCompContext.Campers.ToList();

            if (!camps.Any()) return StatusCode((int)HttpStatusCode.NotFound);

            return Ok(camps);
        }

        [HttpGet("{id}", Name = "GetCamper")]
        public IActionResult Get(int id)
        {
            var camp = _campCompContext.Campers.SingleOrDefault(c => c.Id == id);

            if (camp == null) return NotFound();

            return Ok(camp);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Camper camper)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _logger.LogInformation("Creating a new camp");

            await _campCompContext.Campers.AddAsync(camper);

            var addCampResult = await _campCompContext.SaveChangesAsync();

            if (addCampResult > 0)
            {
                var uri = Url.Link("GetCamper", new { id = camper.Id });

                return Created(uri, camper);
            }

            _logger.LogWarning("Database didn't create the requested camp.");
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public IActionResult Put(int id, [FromBody]Camper camper)
        {
            if (camper == null) return BadRequest();

            var camperQuery = _campCompContext.Campers.SingleOrDefault(c => c.Id == id);

            if (camperQuery == null) return NotFound();

            camperQuery.Age = camper.Age;
            camperQuery.FirstName = camper.FirstName;
            camperQuery.LastName = camper.LastName;

            if (_campCompContext.SaveChanges() > 0) return Ok(camperQuery);

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == default(int)) return BadRequest("id cannot be 0");

            var camper = _campCompContext.Campers.SingleOrDefault(c => c.Id == id);

            if (camper == null) return NotFound();

            _campCompContext.Campers.Remove(camper);

            if(_campCompContext.SaveChanges() > 0) return Ok();

            return StatusCode((int) HttpStatusCode.InternalServerError);
        }
    }
}