using System.Linq;
using AspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/camps/{id}/addresses")]
    public class AddressesController : Controller
    {
        private CompContext _campCompContext;
        private ILogger<CampsController> _logger;

        public AddressesController(CompContext campCompContext, ILogger<CampsController> logger)
        {
            _campCompContext = campCompContext;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var camp = _campCompContext.Campers.Include(c => c.Addresses).SingleOrDefault(c => c.CamperId == id);

            if (camp == null) return NotFound();

            var addresses = camp.Addresses.ToList();

            if (!addresses.Any()) return NotFound();

            //if you don't set reference loop handling you don't get an error, just no response
            return Ok(addresses);
        }
    }
}