using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace CompanyEmployees.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        private readonly LinkGenerator _links;

        public RootController(LinkGenerator linkGenerator)
        {
            _links = linkGenerator;
        }

        [HttpGet(Name ="GetRoot")]
        public IActionResult GetRoot([FromHeader(Name ="Accept")] string mediaType)
        {
            if(mediaType.Contains("application/vnd.testdomain.apiroot"))
            {
                var list = new List<Link>()
                {
                    new Link()
                    {
                        Href =_links.GetUriByName(HttpContext, nameof(GetRoot), new { }),
                        Rel ="self",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href=_links.GetUriByName(HttpContext, "GetCompanies", new{ }),
                        Rel ="companies",
                        Method="GET"
                    },
                    new Link()
                    {
                        Href=_links.GetUriByName(HttpContext,"CreateCompany", new{}),
                        Rel ="create_company",
                        Method="POST"
                    }
                };

                return Ok(list);
            }

            return NoContent();
        }

    }
}
