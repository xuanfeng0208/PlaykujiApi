using Microsoft.AspNetCore.Mvc;

namespace PlayKuji.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChkController : ControllerBase
    {
        [HttpGet]
        public dynamic Get()
        {
            return new { result = "OK" };
        }
    }
}