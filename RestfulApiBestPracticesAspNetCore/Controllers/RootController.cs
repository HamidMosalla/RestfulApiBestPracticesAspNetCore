using System.Collections.Generic;
using AspNetCoreWebApiSamples.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreWebApiSamples.Controllers
{

    [Route("api")]
    //[ApiVersion("1.0")]
    //[Produces("application/json")]
    public class RootController : Controller
    {
        private IUrlHelper _urlHelper;
        private ILogger<RootController> _logger;

        public RootController(IUrlHelper urlHelper, ILogger<RootController> logger)
        {
            _urlHelper = urlHelper;
            _logger = logger;
        }

        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            if (mediaType == "application/vnd.marvin.hateoas+json")
            {
                var links = new List<LinkDto>();

                links.Add(
                  new LinkDto(_urlHelper.Link("GetRoot", new { }),
                  "self",
                  "GET"));

                links.Add(
                 new LinkDto(_urlHelper.Link("GetAuthors", new { }),
                 "authors",
                 "GET"));

                links.Add(
                  new LinkDto(_urlHelper.Link("CreateAuthor", new { }),
                  "create_author",
                  "POST"));

                return Ok(links);
            }

            return NoContent();
        }

        [HttpPost]
        public IActionResult PostRoot()
        {
            _logger.LogError("something something", "so something");
            return Ok();
        }
    }
}
