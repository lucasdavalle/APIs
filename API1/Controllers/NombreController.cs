using API.Model;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NombreController : ControllerBase
    {
        [HttpGet(Name = "GetAppName")]
        public string GetAppName()
        {
            return "CoderProyec";
        }
    }
}
