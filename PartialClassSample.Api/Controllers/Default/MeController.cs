using Microsoft.AspNetCore.Mvc;

namespace PartialClassSample.Api.Controllers.Default
{
    [ApiController, Route("api/[controller]")]
    public class MeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { name = "Partial Class Sample", version = "1.0.0" });
    }
}
